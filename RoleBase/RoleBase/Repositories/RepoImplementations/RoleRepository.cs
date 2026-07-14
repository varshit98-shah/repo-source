using Microsoft.EntityFrameworkCore;
using RoleBase.Data;
using RoleBase.Model;
using RoleBase.Repositories.Interface;
using System.Data;


namespace RoleBase.Repositories.RepoImplementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        /*
        Task<Role> GetByIdAsync(int id);
        Task<Role> GetByNameAsync(string name);
        Task AddAsync(Role role);
        Task DeleteAsync(Role role);
        Task SaveAsync();
        */

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }
        public async Task<Role> GetByIdAsync(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Role> GetByNameAsync(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(x => x.RoleName == name);
        }
        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }
        public async Task DeleteAsync(Role role)
        {
            _context.Roles.Remove(role);

            await Task.CompletedTask;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task AssignRoleToUserAsync(int userId, int roleId)
        {
            var UserRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };

            await _context.UserRoles.AddAsync(UserRole);
        }
        public async Task<string?> GetUserRoleAsync(int userId)
        {
            var role = await _context.UserRoles
                .Where(x => x.UserId == userId)
                .Include(x => x.Role)
                .Select(x => x.Role.RoleName)
                .FirstOrDefaultAsync();

            return role;
        }
    }
}

