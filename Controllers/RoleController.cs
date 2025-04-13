﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_API.Models;
using Task_API.Services.Interfaces;

namespace Task_API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServices _services;
        public RoleController(IRoleServices services)
        {
            _services = services;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var entity = await _services.GetAll();
            return Ok(entity);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _services.GetById(id);
            return Ok(entity);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(Role obj)
        {
            var entity = await _services.Add(obj);
            return Ok(entity);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(Role obj)
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
