using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentProj.Application.DTOs;
using StudentProj.Application.Interfaces;
using StudentProj.Domain.Enums;
using StudentProj.DTO;

namespace StudentProj.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("stats")]
        public async Task<ActionResult> GetDashboardStats()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? User.FindFirst("Email")?.Value;
            var primaryRole = User.FindFirst("PrimaryRole")?.Value ?? "User";

            var stats = await _dashboardService.GetDashboardStatsAsync(email, primaryRole);
            var response = ApiResponse<DashboardStatsDTO>.Create(ResponseStatus.DashboardStatsRetrievedSuccessfully, stats);
            return StatusCode(response.StatusCodes, response);
        }
    }
}
