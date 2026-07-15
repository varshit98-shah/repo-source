using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using RBAC.Authorization;
using RBAC.DTOs;
using RBAC.Models;
using RBAC.Repositories.Implementations;

namespace RBAC.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository repository)
        {
            _userRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await _userRepository.GetAllAsync();
            var result = users.Select(user => new
            {
                user.Id,
                user.Name,
                user.Email,
            });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id) 
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return NotFound();
            return Ok(new
            {
                user.Id,
                user.Name,
                user.Email,
            });
        }

        [HttpGet("{id}/Permission")]
        public async Task<IActionResult> GetUserPermissions(int id)
        {
            var permissions = await _userRepository.GetUserPermissionAsync(id);
            if (!permissions.Any())
                return BadRequest("No permission assigned");

            return Ok(new
            {
                UserId = id,
                TotalPermissions = permissions.Count,
                permissions = permissions
            });
        }

        //[Permission("Create")]

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterDto dto)
        {
            var user = await _userRepository.AddAsync(dto);

            if(user == null)
                return BadRequest("Email already exists");

            return Ok(new
            {
                user.Id,
                user.Name,
                user.Email
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDto dto)
        {
            try
            {
                var user = await _userRepository.UpdateAsync(id, dto, User.Identity?.Name);

                if (user == null)
                    return NotFound();

                return Ok(new
                {
                    user.Id,
                    user.Name,
                    user.Email
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userRepository.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok("User Deleted Successfuly");
        }
    }
}
