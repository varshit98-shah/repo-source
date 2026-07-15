using Microsoft.AspNetCore.Mvc;
using RoleBase.DTOs;
using RoleBase.Repositories.Interface;
using RoleBase.Model;
using Microsoft.AspNetCore.Authorization;

namespace RoleBase.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
   // [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleRepository.GetAllAsync();
            return Ok(roles);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleDto dto)
        {
            var Existrole = await _roleRepository.GetByNameAsync(dto.RoleName);

            if (Existrole != null) 
            {
                return BadRequest("Role already Exists");
            }
            var role = new Role
            {
                RoleName = dto.RoleName
            };
            await _roleRepository.AddAsync(role);
            await _roleRepository.SaveAsync();

            return Ok(role);
        }
        [HttpPost("Assign")]

        public async Task<IActionResult> AssignRoleToUser(AssignRoleDto dto)
        {
            var role = await _roleRepository.GetByIdAsync(dto.RoleId);
            if (role == null)
            {
                return NotFound("Role not found");
            }
            await _roleRepository.AssignRoleToUserAsync(dto.UserId, dto.RoleId);
            await _roleRepository.SaveAsync();
            return Ok("Role assigned to user successfully");
        }




    }
}
