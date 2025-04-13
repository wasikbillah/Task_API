using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_API.Models;
using Task_API.Services.Interfaces;

namespace Task_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly IUserLoginServices _userLoginServices;

        public UserLoginController(IUserLoginServices userLoginServices)
        {
            _userLoginServices = userLoginServices;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login login)
        {
            var token = _userLoginServices.UserLogin(login);

            if (token == "Username and password are required." || token == "Invalid credentials.")
                return Unauthorized(new { message = token });

            return Ok(new
            {
                message = "Login successful",
                token = token
            });

        }

    }
}
