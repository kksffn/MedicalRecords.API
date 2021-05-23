using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MedicalRecords.API.Extensions;
using MedicalRecords.Domain.Extensions;
using MedicalRecords.Domain.Repositories;
using MedicalRecords.Infrastructure.Repositories;
using MedicalRecords.Infrastructure.Extensions;
using MedicalRecords.API.Helpers;

namespace MedicalRecords.API
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
            services
                //.AddEntityFrameworkSqlServer()
                .AddMedicalRecordsContext(Configuration.GetSection("DataSource:ConnectionString").Value)
                .AddScoped<IPatientRepository, PatientRepository>()
                .AddScoped<IRiskFactorRepository, RiskFactorRepository>()
                .AddScoped<IPatientRiskFactorRepository, PatientRiskFactorRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddTokenAuthentication(Configuration) //Infrastructure.Extensions
                .AddMappers()
                .AddServices()
                .AddControllers()
                .AddValidation();

            //Add support for VUE
            services.AddSpaStaticFiles(options => options.RootPath = "client-app/dist");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MedicalRecords.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MedicalRecords.API v1"));
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            //To enable calls from the client
            app.UseCors(cfg =>
            {
                cfg.AllowAnyOrigin();
                cfg.AllowAnyMethod();

            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client-app";
                if (env.IsDevelopment())
                {
                    // Launch development server for Vue.js
                    spa.UseVueDevelopmentServer();
                }
            });
        }
    }
}
