using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    // Extensions methods are used to extend class or methods without changing the original methods from which it is derived
    // Here we will extend the Configuration services method in order to keep startup class as clean as possible
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // we strongly typed a section o app setting file using class and use that as a service.
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

            services.AddScoped<IPhotoService,PhotoService>();

             // we can use singleton for authentication service (TokenService ) class also bu then it will be active till the whole course when our application runs, and so we used Scoped so that it will be active only till the course of http life cycle adn when request is fines service is disposed
            services.AddScoped<ITokenService,TokenServices>(); 
            // We used Interface along with Tokenservice due to reasons below:
            // 1. Interfaces are easy to mock test
            // 2. Testing is easy
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<LogUserActitvity>();

            services.AddScoped<ILikesRepository,LikesRepository>();

            services.AddScoped<IMessageRepository,MessageRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly); // we need to specify to AutoMapper that what classes we need to map to and from for that we have used Helpers/AutoMapperProfiles
             services.AddDbContext<DataContext>(options=>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection")); // UseSQlite is available in using Microsoft.EntityFrameworkCore.Sqlite.Core
            });

            return services;
        }
    }
}