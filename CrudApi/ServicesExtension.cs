using System.Text;
using CrudApi.Authentication;
using InfoLog;
using InfoLog.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CrudApi
{
    public static class ServicesExtension
    {
        public static void AddLogger<T>(this IServiceCollection services, string xmlPath)
            where T : ILogger, new()
        { // документацию по моей библиотеки можно посмотреть в моем репозитории InfoLog
            services.AddSingleton(provider =>
            {
                var configuration = new Configuration(xmlPath);
                var loggerFactory = new LoggerFactory<T>(configuration);
                return loggerFactory.CreateLogger();
            });
        }

        public static void AddJwtSigningAuthentication<T>(this IServiceCollection services, string securityKey) 
            where T: IJwtSigningEncodingKey, new()
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            T jwtSigning = new T { SecretKey = symmetricSecurityKey }; // можно так же установить другой алгоритм шифрования
            // по умолчанию HmacSha256
            services.AddSingleton<IJwtSigningEncodingKey>(jwtSigning); // через него можно будет получать ключ шифрования
            
            const string jwtSchemeName = "JwtBearer";
            services
                .AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = jwtSchemeName;
                    options.DefaultChallengeScheme = jwtSchemeName;
                })
                .AddJwtBearer(jwtSchemeName, jwtBearerOptions => {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = jwtSigning.GetKey(),
 
                        ValidateIssuer = true,
                        ValidIssuer = "CrudApi",
 
                        ValidateAudience = true,
                        ValidAudience = "CrudApiClient",
 
                        ValidateLifetime = true
                    };
                });
        }
    }
}