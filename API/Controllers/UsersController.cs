using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;


        }

        //Endpoint
        /// So we are returning data from API but when it comes to scalability, then what we're doing here
        /// is not really best preactice because this what we're doing here is synchronous code.
        /// And what this means is that when we make a request that goes to our database, then the threads that is 
        /// handling this request is currently blocked.
        /// until a database request is fulfilled now, in our case, it's hard to justify this is because of data.
        /// We have at the moment and what we're doing with the data.But imagine if we had a really complex query that went
        /// to database. If had to fetch a certain number of ecords from a database table that's got 5 million records in it.
        /// It will take longer. And if we are blocking a thread for that particular databse operation, then that's not a good thing
        /// Now, in modern Web servers, they are multi threaded
        /// And let's say, for e.g , you've got an Apache Web servers and it's got 100 threads available.
        /// you're thinking , well, that's no problem, I've got 100 threads to work with. Why can't one of them just wait for the data to come back?
        /// And the answer to that is, well,it's kind of wasteful
        /// Imagine if you had a thousands users, then your application can only serve 100 at any one time and it's wasting resource
        /// that were available. So what we can do is we can make our code asynchronous and we an tell our friend in this case, when
        /// you got to databsae, pass this request onto another thread, that that thread go and deal with getting the data.
        /// But in meant time, if anybody else hits this particular endpoint, I'm going to serve that request.
        /// And if they aslo want something from database, I'm going to pass that off to another thread as well
        /// And what this means is that our code application is instantly more scalable beacuse we make our code, especially
        /// our code, that case, the database, we make it a synchronous.
        /*[HttpGet]
        public ActionResult<IEnumerable<AppUser>> GetUsers()
        {
            return _context.Users.ToList();
            
            
        }*/

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
        {
            //return await _userRepository.GetUsersAsync();// this line will throw type error we need to wrap result into an Ok response since it is actionResult
            // Solution :

            // 1.  var user = await _userRepository.GetUsersAsync();
            //     return Ok(user);
            // OR

            // Shortcut form
            //return Ok(await _userRepository.GetUsersAsync());

            // var users = await _userRepository.GetUsersAsync();

            // var usersToReturn = _mapper.Map<IEnumerable<MemberDTO>>(users);

            // return Ok(usersToReturn);

            var users = await _userRepository.GetMembersAsync();

            return Ok(users);


        }

        //Endpoint 2
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDTO>> GetUser(string username)
        {
           // return await _userRepository.GetUserByUsernameAsync(username); // this we dont need to wrap around OK response.
        //    var user = await _userRepository.GetUserByUsernameAsync(username);

        //    return _mapper.Map<MemberDTO>(user);

          return await _userRepository.GetMemberAsync(username);

        }
    }
}