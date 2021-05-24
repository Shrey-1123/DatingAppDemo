using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper =mapper;

        }

        public async Task<MemberDTO> GetMemberAsync(string username)
        {
            return await _context.Users
            .Where(x => x.UserName == username)
            .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
             
        }

        public async Task<PageList<MemberDTO>> GetMembersAsync(UserParams userParams)
        {
            // var query = _context.Users
            //                 .OrderBy(x=>x.UserName)
            //                 .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
            //                 .AsNoTracking();
            var query = _context.Users.AsQueryable();

             // all users except current logged in user
             query = query.Where(u=>u.UserName != userParams.CurrentUsername).OrderBy(u=>u.UserName);  

             // filter same types of Genders
             query = query.Where(u => u.Gender == userParams.Gender); 

             var minDob = DateTime.Today.AddYears(-userParams.MaxAge -1);
             var maxDob = DateTime.Today.AddYears(-userParams.MinAge);
            
            // filer user by DOB
             query = query.Where(u=>u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);



            return await PageList<MemberDTO>.CreateAsync(query.ProjectTo<MemberDTO>(_mapper.ConfigurationProvider),
             userParams.PageNumber, userParams.PageSize);
        }

        // public async Task<IEnumerable<MemberDTO>> GetMembersAsync()
        // {
        //     return await _context.Users
        //      .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
        //      .ToListAsync();
        // }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
            .Include(p=>p.Photos)
            .SingleOrDefaultAsync(x=>x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            // Eager loading, without eager loading we will gett photos as null
            // But we will se an error in Postman that 500, object cycle detected since we are expecting collections of photos
            // from User entity and in Photo entity it has a User and again a User have Photos collection, To solve this problem we 
            // use a MemeberDTO  and PhotoDTO class
             return await _context.Users
            .Include(p => p.Photos) 
            .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified; // this will update and reflect when entity modified
        }
    }
}