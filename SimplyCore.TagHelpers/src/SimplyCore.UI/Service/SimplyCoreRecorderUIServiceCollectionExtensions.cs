using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Mvc.Razor;

namespace SimplyCore.UI.Service
{
    public static class SimplyCoreRecorderUIServiceCollectionExtensions
    {
        public static IServiceCollection AddSimplyCoreUI(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<ISimplyCoreRecorderUI, SimplyCoreRecorderUIService>();

            return services;
        }
    }
}
