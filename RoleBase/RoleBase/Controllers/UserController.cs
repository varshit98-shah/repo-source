using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoleBase.Attributes;
using RoleBase.DTOs;
using RoleBase.Model;
using RoleBase.Repositories;
using RoleBase.Repositories.Interface;
using RoleBase.Repositories.RepoImplementations;

namespace RoleBase.Controllers
{
   // [Authorize(Roles="Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase    
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository) 
        {
           _userRepository = userRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Add(RegisterDto dto)
        {
            await _userRepository.CreateAsync(dto);
            return Ok(dto);
            
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var all = await _userRepository.GetAllAsync();

            if (all == null)
            {
                return NotFound();
            }

            return Ok(all);
        }
        [HttpDelete("{id}")]
        [RequirePermission("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =  await _userRepository.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Put(UpdateDto user)
        {
            await _userRepository.UpdateAsync(user);
            return Ok();
        }
    }
}
