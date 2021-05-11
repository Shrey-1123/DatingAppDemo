using API.Entities;

namespace API.Interfaces
{
    // this will be single responsibility based 
    public interface ITokenService
    {
         string CreateToken(AppUser user); // this method will recieve AppUser from ENtities
    }
}