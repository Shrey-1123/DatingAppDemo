using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext _context;
        public LikesRepository(DataContext context)
        {
            _context = context;

        }

        // list of users who are being liked
        public async Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
        {
           return await _context.Likes.FindAsync(sourceUserId, likedUserId);
        }

        // this will fetch us the list of users who liked this user
        public async Task<PageList<LikeDTO>> GetUserLikes(LikedParams likedParams)
        {
            var users = _context.Users.OrderBy(u=>u.UserName).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            // we are joinning above two queries to get list of users who liked this user

            if(likedParams.Predicate=="liked") // this is list of users that currently loggeed in User has liked
            {
                likes = likes.Where(likes=>likes.SourceUserId == likedParams.UserId);
                users = likes.Select(like=>like.LikedUser); // likeed Users from like table
            }

            if(likedParams.Predicate=="likedBy")
            {
                likes = likes.Where(likes=>likes.LikedUserId==likedParams.UserId);
                users = likes.Select(like=>like.SourceUser); // this will give us list of users that liked currently loggedIn user
            }

            var likedUsers = users.Select(user=> new LikeDTO{

                Username = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(p=>p.IsMain).Url,
                City = user.City,
                Id = user.Id


            });

            return await PageList<LikeDTO>.CreateAsync(likedUsers, likedParams.PageNumber,likedParams.PageSize);
        }


        // list of users that this user has liked
        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            return await _context.Users
                    .Include(x=>x.LikedUsers)
                    .FirstOrDefaultAsync(x=>x.Id == userId);
        }
    }
}