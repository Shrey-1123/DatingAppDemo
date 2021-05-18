using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public static class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if(await context.Users.AnyAsync())
            {
                return;
            }

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");

            // when we need to out json data we need to use serializer
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            // Iterate though each user in users and add it to database
            foreach(var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd")); // we need to remember that this is seed data
                // that we're using for developing our app. We want to see what ou app is like when
                // we've got users inside it. But these are not real users and we would never create users in this way where we
                // hard code passwords in there. We don;t want to generate random passwords in our seed data because then it's going 
                // to be nightmare to use and log in with these different users because we cannot revert hasvalues of password again.

                user.PasswordSalt = hmac.Key;

                context.Users.Add(user); // we cannot use await here beacuse we are not using anything to track adding of user in database.

            }

            await context.SaveChangesAsync();
        }
    }
}