using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoleBase.Data;
using RoleBase.DTOs;
using RoleBase.Repositories.Interface;

namespace RoleBase.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
       

        public AuthController(
            IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterDto dto)
        {
            var result =
                await _authRepository
                    .RegisterAsync(dto);
            if (!result)
            {
                return BadRequest("Email already exists");
            }

            return Ok(
                "User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token =
                await _authRepository
                    .LoginAsync(dto);

            if (token == null)
            {
                return Unauthorized(
                    "Invalid Credentials");
            }

            return Ok(new
            {
                Token = token
            });
        }
    }
}
