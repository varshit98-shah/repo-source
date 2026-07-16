using StudentProj.Domain.Entities;

namespace StudentProj.Domain.Interfaces
{
    public interface ILogsRepository
    {
        Task<IEnumerable<Logs>> GetLogsAsync(Logs query);
        Task<int> CountRecentLoginsAsync(DateTime since);
        Task<IEnumerable<Logs>> GetRecentLogsAsync(int count, string? email = null, List<string>? userEmails = null);
    }
}
