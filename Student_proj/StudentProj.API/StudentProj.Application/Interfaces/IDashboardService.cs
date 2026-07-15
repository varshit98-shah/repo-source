using StudentProj.Application.DTOs;

namespace StudentProj.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardStatsDTO> GetDashboardStatsAsync();
    }
}
