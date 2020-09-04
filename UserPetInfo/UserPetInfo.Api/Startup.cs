using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetAdoption.Eventing.Extensions;
using UserPetInfo.Api.Consumers;
using UserPetInfo.Api.Policies;
using UserPetInfo.Models;
using UserPetInfo.Models.Config;
using UserPetInfo.Repository;

namespace UserPetInfo.Api
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
            services.AddDbContext<UserPetContext>(options => options.UseSqlServer(Configuration.GetConnectionString(AppConstants.UserPetConnectionStringKey)));

            services.AddAutoMapper(c => c.AddProfile<Mappings>(), typeof(Startup));

            var apiSettingsSection = Configuration.GetSection(ApiSettingsOptions.ApiSettings);

            var apiSettings = apiSettingsSection.Get<ApiSettingsOptions>();
            services.Configure<ApiSettingsOptions>(apiSettingsSection);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"https://{apiSettings?.AuthZeroSettings?.Domain}/";
                options.Audience = apiSettings?.AuthZeroSettings?.Audience;
            });

            services.AddScoped<IUserPetRepository, UserPetRepository>();
            services.AddCustomCorsPolicy();

            // Register consumers
            services.AddHostedService<UserPetCreationConsumer>();
            services.AddEventing((options) =>
            {
                options.SystemUrlList = apiSettings.EventingConfig.SystemUrlList;
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserPetContext dataContext)
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

            // Migrate the DB
            dataContext.Database.Migrate();
        }
    }
}
