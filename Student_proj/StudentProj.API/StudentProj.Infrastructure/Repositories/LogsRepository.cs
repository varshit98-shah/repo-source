using Microsoft.EntityFrameworkCore;
using StudentProj.Domain.Entities;
using StudentProj.Domain.Interfaces;
using StudentProj.Data;

namespace StudentProj.Infrastructure.Repositories
{
    public class LogsRepository : ILogsRepository
    {
        private readonly StudentDbcontext _dbcontext;


        public LogsRepository(StudentDbcontext dbcontext)
        {
            _dbcontext = dbcontext;

        }

        public async Task<IEnumerable<Logs>> GetLogsAsync(Logs query)
        {
            var logs = _dbcontext.Logs.AsQueryable();
            if (!string.IsNullOrEmpty(query.Email)) 
            {
                logs = logs.Where(n => n.Email == query.Email);
            }
            if (!string.IsNullOrEmpty(query.Action))
            {
                logs = logs.Where(n => n.Action.Contains(query.Action));
            }

            var result = await logs
                .OrderByDescending(n => n.Timestamp)
                .ToListAsync();

            return result;
        }

        public async Task<int> CountRecentLoginsAsync(DateTime since)
        {
            return await _dbcontext.Logs
                .Where(l => l.Action != null && l.Action.Contains("Login Succeeded") && l.Timestamp >= since)
                .CountAsync();
        }

        public async Task<IEnumerable<Logs>> GetRecentLogsAsync(int count, string? email = null, List<string>? userEmails = null)
        {
            var query = _dbcontext.Logs.AsQueryable();

            if (userEmails != null && userEmails.Any())
            {
                if (!string.IsNullOrEmpty(email))
                {
                    query = query.Where(l => l.Email == email || userEmails.Contains(l.Email) || string.IsNullOrEmpty(l.Email));
                }
                else
                {
                    query = query.Where(l => userEmails.Contains(l.Email) || string.IsNullOrEmpty(l.Email));
                }
            }
            else if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(l => l.Email == email);
            }

            return await query
                .OrderByDescending(l => l.Timestamp)
                .Take(count)
                .ToListAsync();
        }
    }
}
