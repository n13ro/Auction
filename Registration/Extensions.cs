using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Registration.JWT;
using System.Text;


namespace Registration
{
    /// <summary>
    /// Подключение Auth jwt
    /// </summary>
    public static class Extensions
    {
        public static IServiceCollection AddAuthCustom(this IServiceCollection services, IConfiguration cfg)
        {
            
            var keyBytes = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Jwt__Key"));

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Environment.GetEnvironmentVariable("Jwt__Issuer"),
                    ValidAudience = Environment.GetEnvironmentVariable("Jwt__Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
                };
            });

            services.AddScoped<IJWTService, JWTService>();

            return services;
        }
    }
}
