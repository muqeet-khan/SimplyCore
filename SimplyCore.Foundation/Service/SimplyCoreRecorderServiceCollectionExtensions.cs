using Microsoft.Extensions.DependencyInjection;
using System;

namespace SimplyCore.Foundation.Service
{
    public static class SimplyCoreRecorderServiceCollectionExtensions
    {

        public static IServiceCollection AddSimplyCore(this IServiceCollection services)
        {
            if(services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<ISimplyRecorder, SimplyCoreRecorderService>();

            return services;
        }
        
    }
}
