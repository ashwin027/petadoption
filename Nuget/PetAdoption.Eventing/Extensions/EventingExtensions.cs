using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using PetAdoption.Eventing.Config;
using System;

namespace PetAdoption.Eventing.Extensions
{
    public static class EventingExtensions
    {
        public static void AddEventing(this IServiceCollection services, Action<EventingSystemConfig> config)
        {
            // Add the configuration
            services.Configure(config);

            // Add the producer wrapper
            services.AddScoped<IProducerWrapper, ProducerWrapper>();
        }
    }
}
