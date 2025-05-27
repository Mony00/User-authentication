using Microsoft.AspNetCore.Mvc;
using AuthApiProject.Models;
using AuthApiProject.DTOs;

using BCrypt.Net;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AuthApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // initialized an empty user string for storing users temporary in memory
        // public static List<User> users = new List<User>();

        // Dependency injection of the _context in order to store users in database
        private readonly UserDbContext _context;

        public AuthController(UserDbContext context)
        {
            _context = context;
        }

        //model binding
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO request)
        {
            var existinguser = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (existinguser != null)
            {
                return BadRequest(new { messahe = "Username already exists." });
            }

            // signing the password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // create user object
            var user = new User{
                Username = request.Username,
                PasswordHash = passwordHash
            };

            //save user
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully!" });
        }

         //method to create JWT token during login
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };
            
            // key has to be greater than 512 bit, or 64 charcter to work properly
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsAVeryLongSecretKeyThatShouldBeAtLeastSixtyFourCharactersLong123456"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            // FirstOrDEfaul is a method function from LINQ
            //it find the first item in the collection that matches ther condition and it returns a default value if no match is found(null if object, 0 if int)
            
            if (user == null)
            {
                return BadRequest(new {message = "User not found."});
            }
            if(! BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest(new {message = "Incorrect password!"});
            }

            string token = CreateToken(user);

            return Ok(new {token});
        }

        // [HttpGet("all")]
        // public IActionResult GetAllUsers()
        // {
        //     return Ok(users);
        // }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { message = "API is working!" });
        }

       
    }
}
