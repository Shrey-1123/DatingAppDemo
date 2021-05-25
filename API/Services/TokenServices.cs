using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenServices : ITokenService
    {
        // In order to Inject our configuration in this class we are goona need a constructor and use Iconfiguration
        private readonly SymmetricSecurityKey _key; // SymmetricSecurityKey are types of key where one type of key is used to encrypt and decrypt 
        public TokenServices(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUser user)
        {
            //step 1 : Claim for what you need to authenticate in  our case we are authenticating Username
            var claims = new List<Claim>
            {
                //new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
                // we are going to user Unique Name Id rather than just usrename as ID
                new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName)
            };
            // step 2 : Create some signing credentials and we pass security key and choose security algorithm
             var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);

             // step 3 : Describing Token, inside this we are describe what goes inside our token

             var tokenDescriptor = new SecurityTokenDescriptor
             {
                 Subject = new ClaimsIdentity(claims),
                 Expires = DateTime.Now.AddDays(7),
                 SigningCredentials = creds
             };

             // step 4 :  Creating a token Handler

             var tokenHandler = new JwtSecurityTokenHandler();

             var token = tokenHandler.CreateToken(tokenDescriptor);

             return tokenHandler.WriteToken(token); // return a serliazed token
        }
    }
}
// Note : after adding this service sclass we need to go to stratup class and we'll add service into our dependecny injection