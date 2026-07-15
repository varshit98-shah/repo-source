using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RBAC.Data;
using RBAC.DTOs;
using RBAC.Models;
using RBAC.Repositories.Implementations;

namespace RBAC.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _repository;

        public RoleController(IRoleRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleDto dto)
        {
            var role = new Role
            {
                RoleName = dto.RoleName,
            };
            var result = await _repository.AddAsync(role);
            return Ok(new
            {
                result.RoleName,
                Message = "Role Is created",
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update
            (int id, CreateRoleDto dto)
        {
            var role = new Role
            {
                Id = id,
                RoleName = dto.RoleName
            };

            var result = await _repository.UpdateAsync(role);
            if (result == null)
                return NotFound();

            return Ok(new
            {
                result.RoleName,
                Message = "Role Updated successfully",
            });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _repository.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole(AssignRoleDto dto)
        {
            bool result = await _repository.AssignRoleToUserAsync(dto.UserId, dto.RoleId);

            if (!result)
                return BadRequest("Role Assign Failed");

            return Ok("Role Assign Successfully");
        }

        [HttpPost("assign-permission")]
        public async Task<IActionResult> AssignPermission(AssignPermissionDto dto)
        {
            bool result = await _repository.AssignPermissionAsync(dto.RoleId, dto.PermissionId);

            if (!result)
                return BadRequest("Permission assignment failed.");

            return Ok("Permission Assigned Successfully");
        }
    }
}
