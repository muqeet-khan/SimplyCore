using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using Microsoft.AspNet.Hosting;

namespace SimplyCore.Foundations.Hosting
{
    public class SimplyCoreRecorderMiddleware
    {
        private JsonSerializer serializer;
        private IHostingEnvironment _env;
        private ILogger _logger;
        private readonly RequestDelegate _next;

        public SimplyCoreRecorderMiddleware(RequestDelegate next, ILogger<SimplyCoreRecorderMiddleware> logger, IHostingEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Value.Contains("!SimplyCore"))
            {
                serializer = new JsonSerializer();
                serializer.Converters.Add(new JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;
                serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                if (!Directory.Exists(@"../SimplyCoreLogs"))
                {
                    Directory.CreateDirectory(@"../SimplyCoreLogs");
                }

                var path = @"../SimplyCoreLogs/log_" + DateTime.UtcNow.ToString("yyyyMMddHHmmssfff") + ".json";

                PathString strippedPath = httpContext.Request.Path.Value.Split(new string[] { "!SimplyCore" }, StringSplitOptions.None)[0];
                httpContext.Request.Path = new PathString(strippedPath);
                await _next.Invoke(httpContext);

                using (StreamWriter sw = new StreamWriter(File.Create(path)))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartObject();
                    writer.WritePropertyName("request");
                    writer.WriteStartObject();
                    writer.WritePropertyName("headers");
                    serializer.Serialize(writer, httpContext.Request.Headers);
                    writer.WriteEndObject();
                    writer.WritePropertyName("response");
                    writer.WriteStartObject();
                    writer.WritePropertyName("headers");
                    serializer.Serialize(writer, httpContext.Response.Headers);
                    writer.WriteEndObject();
                    writer.WriteEndObject();
                }
                await httpContext.Response.WriteAsync("<h1>Thank You! Your web request was recorded</h1>");
            }
            else
            {
                await _next(httpContext);
            }
        }
    }
}
