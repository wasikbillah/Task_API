using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_API.Models;
using Task_API.Services.Interfaces;

namespace Task_API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionServices _services;
        public RolePermissionController(IRolePermissionServices services)
        {
            _services = services;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var entity = await _services.GetAll();
            var result = entity.Select(u => new
            {
                u.Id,
                RoleName = u.Role?.Name ?? "N/A",
                PermissionName = u.Permission?.Name ?? "N/A"
            });
            return Ok(result);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var permissions = await _services.GetById(id);

            var result = permissions.Select(p => new
            {
                p.Id,
                RoleName = p.Role?.Name ?? "N/A",
                PermissionName = p.Permission?.Name ?? "N/A"
            });

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(RolePermission obj)
        {
            var entity = await _services.Add(obj);
            return Ok(entity);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(RolePermission obj)
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
