using Microsoft.AspNet.Builder;

namespace SimplyCore.Foundation.Hosting
{
    public static class SimplyCoreRecorderMiddlewareExtensions
    {
        // Extension method used to add the middleware to the HTTP request pipeline.
        public static IApplicationBuilder UseSimplyCoreRecorder(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SimplyCoreRecorderMiddleware>();
        }

        public static IApplicationBuilder UseSimplyCoreRecorder(this IApplicationBuilder builder,SimplyCoreRecorderOptions options)
        {
            return builder.UseMiddleware<SimplyCoreRecorderMiddleware>(options);
        }
    }
}
