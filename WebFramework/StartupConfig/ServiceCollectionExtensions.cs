using Common.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;
namespace WebFramework.StartupConfig
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            var jwtSettings = configuration .GetSection("JwtSettings") .Get<JwtSettings>();

            var secretKey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

            var encryptionKey = Encoding.UTF8.GetBytes(jwtSettings.EncryptionKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero,

                        RequireSignedTokens = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                        RequireExpirationTime = true,
                        ValidateLifetime = true,

                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,

                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,

                        TokenDecryptionKey =new SymmetricSecurityKey(encryptionKey)
                    };
            });

        }

            
    }
}
