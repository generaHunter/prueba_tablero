using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using tablero.Application.External.JWT;
using tablero.External.GetTokenJWT;

namespace tablero.External
{
    public static class DependecyInjectionService
    {
        public static IServiceCollection AddExternal(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IGetTokenJWTService, GetTokenJWTService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKeyJWT"] ?? string.Empty)),
                    ValidIssuer = configuration["JWT:IssuerJWT"],
                    ValidAudience = configuration["JWT:AudienceJWT"]
                };
            });
            return services;
        }
    }
}
