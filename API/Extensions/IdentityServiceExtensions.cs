using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    // Extensions methods are used to extend class or methods without changing the original methods from which it is derived
    // Here we will extend the Configuration services method in order to keep startup class as clean as possible
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            // System.InvalidOperationException: No authenticationScheme was specified,
            // and there was no DefaultChallengeScheme found. The default schemes can be set using either 
            // Above error we will get when even after generating TOken when we call the Authorize Endpoint for UserController
            // for that we need to tell AuthenticationScheme
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>{

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                    ValidateIssuer = false, // this will be API server*/
                    ValidateAudience = false, // this will be our angular application

                };
            });

            return services;
        }
    }
}