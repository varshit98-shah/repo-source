using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleBase.Data;
using RoleBase.DTOs;
using RoleBase.Model;
using RoleBase.Repositories.Interface;


namespace RoleBase.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {

        private readonly IPermissionRepository _permissionRepository;


        public PermissionController(IPermissionRepository permissionRepository )
        {
            _permissionRepository = permissionRepository;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            var permissions = await _permissionRepository.GetAllAsync();
            return Ok(permissions);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePermission(PermissionDto dto)
        {
            var ExtPermission = await _permissionRepository.GetByNameAsync(dto.PermissionName);

            if (ExtPermission != null)
            {
                return BadRequest("Permission already exists.");
            }

            var per = new Permission
            {
                PermissionName = dto.PermissionName
            };

            await _permissionRepository.AddAsync(per);
            await _permissionRepository.SaveAsync();
            return Ok("Permission Created ");
        }

        [HttpPost("Assign")]
        public async Task<IActionResult> AssignPermission(AssignPermissionDto dto)
        {
            await _permissionRepository.AssignPermissionToRoleAsync(dto.RoleId, dto.PermissionId);

            await _permissionRepository.SaveAsync();

            return Ok("Permission assigned to role successfully.");

        }
        [HttpGet("{userId}")]

        public async Task<IActionResult> GetUserPermission(int userId) 
        {
            
            var aa = await _permissionRepository.GetPermissionsByUserIdAsync(userId);

            if(aa == null) {  return BadRequest("No Id found  "); }

            return Ok(

                   new
                   {
                       userId = userId,
                       TotelPermission = aa.Count,
                       permissions = aa

                   }

                );
      
        }




    }
}
