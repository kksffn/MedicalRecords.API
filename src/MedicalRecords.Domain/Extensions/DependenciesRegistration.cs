using System.Reflection;
using MedicalRecords.Domain.Mappers;
using MedicalRecords.Domain.Services;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;


namespace MedicalRecords.Domain.Extensions
{
    public static class DependenciesRegistration
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services
                .AddSingleton<IPatientMapper, PatientMapper>()
                .AddSingleton<IRiskFactorMapper, RiskFactorMapper>()
                .AddSingleton<IPatientRiskFactorMapper, PatientRiskFactorMapper>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<IPatientService, PatientService>()
                .AddScoped<IRiskFactorService, RiskFactorService>()
                .AddScoped<IPatientRiskFactorService, PatientRiskFactorService>()
                .AddScoped<IUserService, UserService>(); 
            return services;
        }

        public static IMvcBuilder AddValidation(this IMvcBuilder builder)
        {
            builder
                .AddFluentValidation(configuration =>
                    configuration.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
            return builder;
        }
    }
}
