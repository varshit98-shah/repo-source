using StudentProj.Application.DTOs;
using StudentProj.Application.Interfaces;
using StudentProj.Domain.Interfaces;

namespace StudentProj.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IStudent _studentRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly ISubjectRepository _subjectRepo;
        private readonly ILogsRepository _logsRepo;

        public DashboardService(
            IStudent studentRepo,
            ICourseRepository courseRepo,
            ISubjectRepository subjectRepo,
            ILogsRepository logsRepo)
        {
            _studentRepo = studentRepo;
            _courseRepo = courseRepo;
            _subjectRepo = subjectRepo;
            _logsRepo = logsRepo;
        }

        public async Task<DashboardStatsDTO> GetDashboardStatsAsync(string? email, string primaryRole)
        {
            var studentsCount = await _studentRepo.CountAsync();
            var coursesCount = await _courseRepo.CountAsync();
            var subjectsCount = await _subjectRepo.CountAsync();
            
            var recentLogins = await _logsRepo.CountRecentLoginsAsync(DateTime.UtcNow.AddDays(-7));

            List<string>? userEmails = null;
            if (primaryRole == "Admin")
            {
                userEmails = await _studentRepo.GetStudentEmailsByRoleAsync("User");
            }

            var recentLogs = await _logsRepo.GetRecentLogsAsync(
                count: 20, 
                email: primaryRole != "Super Admin" ? email : null, 
                userEmails: userEmails
            );

            var recentActivities = recentLogs.Select(l => new LogDTO 
            {
                Id = l.Id,
                Username = l.Name,
                Email = l.Email,
                Action = l.Action,
                IpAddress = l.IpAddress,
                Timestamp = l.Timestamp
            }).ToList();

            return new DashboardStatsDTO
            {
                TotalStudents = studentsCount,
                ActiveCourses = coursesCount,
                TotalSubjects = subjectsCount,
                RecentLogins = recentLogins,
                RecentActivities = recentActivities
            };
        }
    }
}
