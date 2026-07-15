using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RBAC.DTOs;
using RBAC.Migrations;
using RBAC.Models;
using RBAC.Repositories.Interfaces;

namespace RBAC.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionRepository _repository;

        public PermissionController(IPermissionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var permission = await _repository.GetAllAsync();
            var result = permission.Select(x => new
                {   
                    x.Id,
                    x.PermissionName,
                });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePermissionDto dto)
        {
            var permission = new Permission
            {
                PermissionName = dto.PermissionName
            };
            var result = await _repository.AddAsync(permission);
            return Ok(new
            {
                result.Id,
                result.PermissionName,
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreatePermissionDto dto)
        {
            try
            {
                var result = await _repository.UpdateAsync(id, dto);

                if (result == null)
                    return NotFound();

                return Ok(new
                {
                    result.Id,
                    result.PermissionName
                });
            }
            catch
            {
                return BadRequest("something Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _repository.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok("Role Deleted Successfully");
        }
    }
}
