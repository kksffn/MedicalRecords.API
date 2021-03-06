using System.Text;
using MedicalRecords.Domain.Entities;
using MedicalRecords.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace MedicalRecords.Infrastructure.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services,
              IConfiguration configuration)
        {
            var settings = configuration.GetSection("AuthenticationSettings");
            var settingsTyped = settings.Get<AuthenticationSettings>();

            services.Configure<AuthenticationSettings>(settings);
            var key = Encoding.ASCII.GetBytes(settingsTyped.Secret);

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<MedicalRecordsContext>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false//,
                        //ValidIssuer = "https://localhost:44370",
                        //ValidAudience = "https://localhost:44370"
                    };
                });

            return services;
        }
    }
}
