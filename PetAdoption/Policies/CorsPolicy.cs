using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace PetAdoption.Policies
{
    public static class CorsPolicy
    {
        public const string CorsPolicyKey = "CorsPolicy";
        public static void AddCustomCorsPolicy(this IServiceCollection services, string[] allowedOrigins)
        {
            services.AddCors(o => o.AddPolicy(CorsPolicyKey, builder =>
            {
                builder.WithOrigins(allowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }
    }
}
