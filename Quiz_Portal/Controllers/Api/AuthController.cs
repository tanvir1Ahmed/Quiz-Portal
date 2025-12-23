using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz_Portal.Data;
using Quiz_Portal.Models;
using Quiz_Portal.Models.DTOs;

namespace Quiz_Portal.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly QuizDbContext _context;

        public AuthController(QuizDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Sign up a new user
        /// </summary>
        [HttpPost("signup")]
        public async Task<ActionResult<object>> SignUp([FromBody] UserLoginDto signupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add +880 prefix to phone number for storage
            var fullPhoneNumber = "+880" + signupDto.PhoneNumber;

            // Check if user already exists by phone number
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == fullPhoneNumber);

            if (existingUser != null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "An account with this phone number already exists. Please login instead."
                });
            }

            // Create new user
            var newUser = new User
            {
                Name = signupDto.Name,
                PhoneNumber = fullPhoneNumber,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Registration successful! You can now login.",
                user = new
                {
                    newUser.Id,
                    newUser.Name,
                    newUser.PhoneNumber
                }
            });
        }

        /// <summary>
        /// Login an existing user
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<object>> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add +880 prefix to phone number for lookup
            var fullPhoneNumber = "+880" + loginDto.PhoneNumber;

            // Check if user exists by phone number
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == fullPhoneNumber);

            if (existingUser == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "No account found with this phone number. Please sign up first."
                });
            }

            return Ok(new
            {
                success = true,
                message = "Welcome back!",
                user = new
                {
                    existingUser.Id,
                    existingUser.Name,
                    existingUser.PhoneNumber
                }
            });
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        [HttpGet("user/{id}")]
        public async Task<ActionResult<object>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new { success = false, message = "User not found" });
            }

            return Ok(new
            {
                success = true,
                user = new
                {
                    user.Id,
                    user.Name,
                    user.PhoneNumber
                }
            });
        }
    }
}
