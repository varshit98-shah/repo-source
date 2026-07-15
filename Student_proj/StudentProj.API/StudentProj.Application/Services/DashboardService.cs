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

        public async Task<DashboardStatsDTO> GetDashboardStatsAsync()
        {
            var students = await _studentRepo.GetAllStudentsasync();
            var courses = await _courseRepo.GetAllAsync();
            var subjects = await _subjectRepo.GetAllAsync();
            
            var logs = await _logsRepo.GetLogsAsync(new StudentProj.Domain.Entities.Logs());
            var recentLogins = logs.Count(l => l.Action != null && l.Action.Contains("Login Succeeded") && l.Timestamp >= DateTime.UtcNow.AddDays(-7));

            return new DashboardStatsDTO
            {
                TotalStudents = students.Count,
                ActiveCourses = courses.Count(),
                TotalSubjects = subjects.Count(),
                RecentLogins = recentLogins
            };
        }
    }
}
