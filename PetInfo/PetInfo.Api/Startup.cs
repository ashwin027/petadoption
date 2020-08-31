using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetInfo.Api.Policies;
using PetInfo.Models;
using PetInfo.Models.Config;
using PetInfo.Repository;

namespace PetInfo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApiSettingsOptions>(Configuration.GetSection(ApiSettingsOptions.ApiSettings));

            var apiSettings = Configuration.GetSection(ApiSettingsOptions.ApiSettings).Get<ApiSettingsOptions>();
            var dogEndpointInfo = apiSettings?.Endpoints?.FirstOrDefault(x => x.Type.Equals(AnimalType.Dogs));

            services.AddHttpClient<IDogBreedInfoRepository, DogBreedInfoRepository>(nameof(DogBreedInfoRepository),
                client =>
                {
                    if (!string.IsNullOrWhiteSpace(dogEndpointInfo?.BaseUrl))
                    {
                        client.BaseAddress = new Uri(dogEndpointInfo.BaseUrl);
                    }
                    client.DefaultRequestHeaders.Add(Constants.ApiKey, dogEndpointInfo?.ApiKey);
                });

            services.AddAutoMapper(c => c.AddProfile<Mappings>(), typeof(Startup));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"https://{apiSettings?.AuthZeroSettings?.Domain}/";
                options.Audience = apiSettings?.AuthZeroSettings?.Audience;
            });

            // Allowing CORS temporarily. Need to switch to allowed origins configured in app settings
            services.AddCustomCorsPolicy();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(CorsPolicy.CorsPolicyKey);
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
