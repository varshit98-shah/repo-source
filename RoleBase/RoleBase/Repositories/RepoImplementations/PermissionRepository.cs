using RoleBase.Data;
using RoleBase.Model;
using RoleBase.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using RoleBase.DTOs;

namespace RoleBase.Repositories.RepoImplementations
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public PermissionRepository(ApplicationDbContext context) 
        {
          _context = context;
        }
        /*
        Task<IEnumerable<Permission>> GetAllAsync();
        Task<IEnumerable<Permission>> GetByIdAsync(int id);
        Task<IEnumerable<Permission>> GetByNameAsync(string name);
        Task AddAsync(Permission permission);
        Task DeleteAsync(Permission permission);
        Task SaveAsync();
        */

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await _context.Permissions.ToListAsync();
        }
        public async Task<IEnumerable<Permission>> GetByIdAsync(int id)
        {
            return await _context.Permissions.Where(x => x.Id == id).ToListAsync();
        }
        public async Task<Permission?> GetByNameAsync(string name)
        {
            return await _context.Permissions
                                 .FirstOrDefaultAsync(x => x.PermissionName == name);
        }
        public async Task DeleteAsync(Permission permission) 
        {
            _context.Permissions.Remove(permission);

            await Task.CompletedTask;
        }
        public async Task AddAsync(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
        }
        public async Task SaveAsync() 
        { 
            await _context.SaveChangesAsync(); 
        }
        public async Task AssignPermissionToRoleAsync(int RoleId, int PermissionId) 
        {
            var rolePermission = new RolePermission
            {
                PermissionId = PermissionId,
                RoleId = RoleId,
            };
            await _context.RolePermissions.AddAsync(rolePermission);
          
        }
        public async Task<List<Permission>> GetPermissionsByUserIdAsync(int userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .SelectMany(ur => ur.Role.RolePermission)
                .Select(rp => rp.Permission)
                .Distinct()
                .ToListAsync();
        }

    }
}
