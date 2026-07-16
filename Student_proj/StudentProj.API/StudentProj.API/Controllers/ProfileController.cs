using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentProj.Application.DTOs;
using StudentProj.Application.Interfaces;
using StudentProj.Domain.Enums;
using StudentProj.DTO;
using System.Security.Claims;

namespace StudentProj.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IStudentService _service;

        public ProfileController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userIdStr = User.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
            {
                var bad = ApiResponse<object>.Create(ResponseStatus.BadRequest, "User ID not found in token");
                return StatusCode(bad.StatusCodes, bad);
            }

            var student = await _service.GetStudentbyid(userId);
            if (student == null)
            {
                var bad = ApiResponse<object>.Create(ResponseStatus.UserNotFound);
                return StatusCode(bad.StatusCodes, bad);
            }
            var response = ApiResponse<object>.Create(ResponseStatus.UserRetriveSuccessfully, student);
            return StatusCode(response.StatusCodes, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] StudentDTO dto)
        {
            var userIdStr = User.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
            {
                var bad = ApiResponse<object>.Create(ResponseStatus.BadRequest, "User ID not found in token");
                return StatusCode(bad.StatusCodes, bad);
            }

            // Ensure the user is only updating their own profile
            if (dto.Id != userId)
            {
                var bad = ApiResponse<object>.Create(ResponseStatus.BadRequest, "You can only update your own profile");
                return StatusCode(bad.StatusCodes, bad);
            }

            var updatedBy = User.FindFirst("Name")?.Value ?? "System";
            var ipAddress = StudentProj.Domain.Common.IpHelper.GetClientIpAddress(HttpContext);

            var (success, error) = await _service.UpdateStudentasync(userId, dto, updatedBy, ipAddress);
            if (!success)
            {
                var bad = ApiResponse<object>.Create(ResponseStatus.BadRequest, error ?? "Failed to update profile");
                return StatusCode(bad.StatusCodes, bad);
            }
            var response = ApiResponse<object>.SuccessResponse(true, "Profile updated successfully.");
            return StatusCode(response.StatusCodes, response);
        }
    }
}
