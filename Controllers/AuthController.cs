using HRMS.Backend.Data;
using HRMS.Backend.DTOs;
using HRMS.Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace HRMS.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly HRMSDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthController(HRMSDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<User>();
        }

        // REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

                if (existingUser != null)
                    return Conflict(new { message = "Email already exists" });

                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    Phone = model.Phone,
                    Address = model.Address,
                    IsActive = true
                };

                user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "User registered successfully",
                    user = new
                    {
                        user.Id,
                        user.Username,
                        user.Email,
                        user.Phone,
                        user.Address
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error during registration", error = ex.Message });
            }
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null || !user.IsActive)
                    return Unauthorized(new { message = "Invalid credentials" });

                var result = _passwordHasher.VerifyHashedPassword( user, user.PasswordHash, model.Password );
                if (result == PasswordVerificationResult.Failed)
                    return Unauthorized(new { message = "Invalid credentials" });

                var token = GenerateJwtToken(user);
                return Ok(new{ message = "Login successful", token });
            }
            catch (Exception ex)
            {
                return BadRequest(new{ message = "Error during login", error = ex.Message });
            }
        }

        // LOGOUT
        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logged out successfully" });
        }

        // JWT GENERATOR
        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["DurationInMinutes"] ?? "60")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}