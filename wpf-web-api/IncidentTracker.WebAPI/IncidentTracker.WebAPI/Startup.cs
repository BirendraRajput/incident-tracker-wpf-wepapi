using IncidentTracker.BusinessEntitiy;
using IncidentTracker.DAL;
using IncidentTracker.DAL.Repositiry;
using IncidentTracker.WebAPI.ExceptionMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace IncidentTracker.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

      
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
                     

            //Adding Swageer service

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Incident Tracker API", Version = "V1" });
                              
            });

            // Add service and create Policy with options
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            // configure unity related settings
            services.AddTransient<IncidentContext, IncidentContext>();
            services.AddTransient<IGenericRepository<Incident>, GenericRepository<Incident>>();
            services.AddTransient<IGenericRepository<Location>, GenericRepository<Location>>();
            services.AddTransient<IIncidentDAL, IncidentDAL>();            
            services.AddTransient<ILocationDAL, LocationDAL>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Incident Tracker API");
            });

             app.UseCors("CorsPolicy");

             app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ConfigureCustomExceptionMiddleware();
        }
    }
}
