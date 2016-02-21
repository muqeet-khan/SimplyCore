using Microsoft.AspNet.Builder;

namespace SimplyCore.UI.Hosting
{
    public static class SimplyRecorderUIMiddlewareExtensions
    {

        public static IApplicationBuilder UseSimplyCoreRecorderUI(this IApplicationBuilder builder)
        {

            builder.Map("/SimplyCore", HandleUI);

            return builder;
            
        }

        private static void HandleUI(IApplicationBuilder app)
        {
            app.UseMiddleware<SimplyRecorderUIMiddleware>();
            
            app.UseDeveloperExceptionPage();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "SimplyCore",
                    template: "{controller=SimplyCore}/{action=UI}/{id?}");
            });

        }
    }
}
