using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RBAC.DTOs;
using RBAC.Models;
using RBAC.Repositories.Interfaces;

namespace RBAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        
        public AuthController(IAuthRepository repository)
        {
            _repository = repository;
        }

        //[AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _repository.Register(dto);

            if (!result)
                return BadRequest("Email Already Exists");

            return Ok("User Register Successfully");
        }

        //[AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _repository.Login(dto);
            if (token == null)
                return Unauthorized("Invaid Email or Password.");

            
            return Ok(new
            {
                Token = token,
                message = "User Login successfully"
            });
        }
    }
}
