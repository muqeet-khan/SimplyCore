using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System.IO;
using System.Net;
using System.Text;
using System.Reflection;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Extensions.OptionsModel;
using Microsoft.AspNet.FileProviders;
using SimplyCore.UI.Service;
using Microsoft.AspNet.Routing;

namespace SimplyCore.UI.Hosting
{
    // You may need to install the Microsoft.AspNet.Http.Abstractions package into your project
    public class SimplyRecorderUIMiddleware
    {
        private readonly Assembly _assembly;
        private readonly RequestDelegate _next;
        private IOptions<RazorViewEngineOptions> _razor;

        public SimplyRecorderUIMiddleware(RequestDelegate next)
        {
            _next = next;
            _assembly = GetType().GetTypeInfo().Assembly;

        }

        public async Task Invoke(HttpContext httpContext, IOptions<RazorViewEngineOptions> razor)
        {
            

            _razor = razor;
            _razor.Value.FileProvider = new EmbeddedFileProvider(_assembly);
            //_razor.Value.ViewLocationExpanders.Add(new UIViewLocationExpander());


            await _next(httpContext);
            
            //var responseStream =  _assembly.GetManifestResourceStream("SimplyCore.UI.Views.response.html");
            //httpContext.Response.StatusCode = 200;
            //httpContext.Response.ContentType = "text/html";
            //await responseStream.CopyToAsync(httpContext.Response.Body);
            
            //await httpContext.Response.WriteAsync("Map Test Successful");
        }

        //private static async Task ReturnIndexPage(HttpContext context)
        //{
        //    var file = new FileInfo(@"wwwroot\response.html");
        //    byte[] buffer;
        //    if (file.Exists)
        //    {
        //        context.Response.StatusCode = (int)HttpStatusCode.OK;
        //        context.Response.ContentType = "text/html";

        //        buffer = File.ReadAllBytes(file.FullName);
        //    }
        //    else
        //    {
        //        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        //        context.Response.ContentType = "text/plain";
        //        buffer = Encoding.UTF8
        //            .GetBytes("Unable to find the requested file");
        //    }

        //    using (var stream = context.Response.Body)
        //    {
        //        await stream.WriteAsync(buffer, 0, buffer.Length);
        //        await stream.FlushAsync();
        //    }

        //    context.Response.ContentLength = buffer.Length;
        //}
    }
}
