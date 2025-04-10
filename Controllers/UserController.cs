using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_API.Models;
using Task_API.Services.Interfaces;

namespace Task_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _services;
        public UserController(IUserServices services)
        {
            _services = services;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _services.GetAll();
            var result = entities.Select(u => new
            {
                u.Id,
                u.Name,
                u.RoleId,
                RoleName = u.Role?.Name ?? "N/A"
            });
            return Ok(result);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _services.GetById(id);
            return Ok(entity);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(User obj)
        {
            var entity = await _services.Add(obj);
            return Ok(entity);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(User obj)
        {
            var entity = await _services.Update(obj);
            return Ok(entity);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _services.Delete(id);
            return Ok(entity);
        }
    }

}
