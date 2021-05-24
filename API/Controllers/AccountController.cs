using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(DataContext context, ITokenService tokenService, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _tokenService = tokenService;

        }

        /* [HttpPost("register")]
         public async Task<ActionResult<AppUser>> Register(string username,string password) // one of the things [ApiController] attributes does is it binds the data provide inside this method as paramter
         {
             using var hmac = new HMACSHA512();
             var user = new AppUser
             {
                 UserName = username,
                 PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)), // GetBytes converts string into byte[]
                 PasswordSalt = hmac.Key
             };
             _context.Users.Add(user);
             await _context.SaveChangesAsync();

             return user;
         }*/
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO) // one of the things [ApiController] attributes does is it binds the data provide inside this method as paramter
        {
            if (await UserExists(registerDTO.Username))
            {
                return BadRequest("Username taken");
            }

            var user = _mapper.Map<AppUser>(registerDTO);
            using var hmac = new HMACSHA512();
            
                user.UserName = registerDTO.Username.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password));// GetBytes converts string into byte[]
                user.PasswordSalt = hmac.Key;
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDTO
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };
        }

        /*[HttpPost("login")]
        public async Task<ActionResult<AppUser>> Login(LoginDTO loginDTO)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x=>x.UserName==loginDTO.Username);
            if(user==null)
            {
                return Unauthorized("Invalid Username");
            }
            using var hmac = new HMACSHA512(user.PasswordSalt); // when we create we create new instance of this class the default ctor will generate a random
            // key , but during login we need to match the salted password with db,
            // solution to this problem is an overload ctor which takes a key as byte[] 
            // and this key will generate the same salted hash password that was generate during registration for that user
            // because this will use the same secret key.

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            // point is that if input password is same as during reigtration then it will create same hashcode
            // for computedHash and we can compare the hascode of stored password from database if it matches then user is Valid

            //since out computeHash/ salted password is byte[] we need to loop over
            for(int i=0;i<computedHash.Length;i++)
            {
                if(computedHash[i]!=user.PasswordHash[i])
                {
                    return Unauthorized("Invalid User");
                }
            }

            return user; // Here we see we are returning a user of AppUser type before adding token so we need to create another DTO class containting username and TOken
        }*/
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == loginDTO.Username);
            if (user == null)
            {
                return Unauthorized("Invalid Username");
            }
            using var hmac = new HMACSHA512(user.PasswordSalt); // when we create we create new instance of this class the default ctor will generate a random
            // key , but during login we need to match the salted password with db,
            // solution to this problem is an overload ctor which takes a key as byte[] 
            // and this key will generate the same salted hash password that was generate during registration for that user
            // because this will use the same secret key.

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            // point is that if input password is same as during reigtration then it will create same hashcode
            // for computedHash and we can compare the hascode of stored password from database if it matches then user is Valid

            //since out computeHash/ salted password is byte[] we need to loop over
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid User");
                }
            }

            return new UserDTO
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = user.KnownAs,
                Gender = user.Gender
            }; // After adding token things
        }

        private async Task<bool> UserExists(string username)
        {

            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower()); // check any username in out table matche with our username
                                                                                         //return await _context.Users.FindAsync(x => x.UserName==username.ToLower());
                                                                                         // Findasync is used to get data by primary key

        }


    }
}
