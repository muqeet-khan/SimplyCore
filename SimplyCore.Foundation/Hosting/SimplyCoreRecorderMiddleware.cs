﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.OptionsModel;
using SimplyCore.Foundation.Service;

namespace SimplyCore.Foundation.Hosting
{
    public class SimplyCoreRecorderMiddleware
    {
        private JsonSerializer serializer;
        private IHostingEnvironment _env;
        private ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly SimplyCoreRecorderOptions _options;

        public SimplyCoreRecorderMiddleware(RequestDelegate next, ILogger<SimplyCoreRecorderMiddleware> logger, 
                        IHostingEnvironment env,SimplyCoreRecorderOptions options)
        {
            _next = next;
            _logger = logger;
            _env = env;
            _options = options;
        }

        public async Task Invoke(HttpContext httpContext,ISimplyRecorder simply)
        {
            if (httpContext.Request.Path.Value.EndsWith(_options.MatchPattern))
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

                PathString strippedPath = httpContext.Request.Path.Value.Split(new string[] { _options.MatchPattern }, StringSplitOptions.None)[0];
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
                    writer.WritePropertyName("customdata");
                    writer.WriteStartObject();
                    writer.WritePropertyName("dataItem");
                    serializer.Serialize(writer, simply.GetAllInfo());
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
