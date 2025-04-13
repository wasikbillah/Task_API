using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_API.Models;
using Task_API.Services.Interfaces;

namespace Task_API.Controllers
{
    [Authorize(Roles = "Admin,Project Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskServices _services;
        public TaskController(ITaskServices services)
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
                u.Title,
                u.Description,
                UserName = u.AssignedUser?.Name ?? "N/A",
                Status = u.Status?.Name ?? "N/A",
                StartDate = u.StartDate.ToString("dd/MM/yyyy HH:mm:ss"),
                Deadline = u.Deadline.ToString("dd/MM/yyyy HH:mm:ss")   
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
        public async Task<IActionResult> Add(Models.Task obj)
        {
            var entity = await _services.Add(obj);
            return Ok(entity);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(Models.Task obj)
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
