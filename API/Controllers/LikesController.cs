using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class LikesController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly ILikesRepository _likesRepository;
        public LikesController(IUserRepository userRepository, ILikesRepository likesRepository)
        {
           _likesRepository = likesRepository;
           _userRepository = userRepository;

        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
            var sourceUserId = User.GetUserId();
            var likedUser = await _userRepository.GetUserByUsernameAsync(username);
            var sourceUser = await _likesRepository.GetUserWithLikes(sourceUserId);

            if(likedUser == null)
            {
                return NotFound();
            }

            if(sourceUser.UserName == username) // if user liking himself
            {
                return BadRequest("You cannot like yourself");
            }

            var userLike = await _likesRepository.GetUserLike(sourceUserId,likedUser.Id);

            if(userLike !=null)
            {
                return BadRequest("you already like this user");
            }

            userLike = new Entities.UserLike{

                SourceUserId = sourceUserId,
                LikedUserId = likedUser.Id
            };

            sourceUser.LikedUsers.Add(userLike);

            if(await _userRepository.SaveAllAsync())
            {
                return Ok();
            }

            return BadRequest("Failed to like user");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDTO>>> GetUserLikes([FromQuery]LikedParams likedParams)
        {
            likedParams.UserId = User.GetUserId();
            var users = await _likesRepository.GetUserLikes(likedParams);

            // to recieve paginated repsonse

            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);


            return Ok(users);
        }
    }

    
}