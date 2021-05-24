using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IUserRepository
    {
         void Update(AppUser user);
         Task<bool> SaveAllAsync();
         Task<IEnumerable<AppUser>> GetUsersAsync();
         Task<AppUser> GetUserByIdAsync(int id);
         Task<AppUser> GetUserByUsernameAsync(string username);
        //Task<IEnumerable<MemberDTO>> GetMembersAsync();
        Task<PageList<MemberDTO>> GetMembersAsync(UserParams userParams);

         Task<MemberDTO> GetMemberAsync(string username);
    }
}