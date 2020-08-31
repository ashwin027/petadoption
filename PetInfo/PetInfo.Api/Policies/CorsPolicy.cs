using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace PetInfo.Api.Policies
{
    public static class CorsPolicy
    {
        public const string CorsPolicyKey = "CorsPolicy";
        public static void AddCustomCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy(CorsPolicyKey, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }
    }
}
