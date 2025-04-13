using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Task_API.Data;
using Task_API.Models;
using Task_API.Services.Interfaces;

namespace Task_API.Services.Implementations
{
    public class UserLoginServices : IUserLoginServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserLoginServices(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string UserLogin(Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
            {
                return "Username and password are required.";
            }

            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Name == login.Username);
            if (user == null || user.Password != login.Password)
            {
                return "Invalid credentials.";
            }

            // Generate JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.Name),
            }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiryInMinutes"])),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}
