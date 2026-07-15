using Microsoft.EntityFrameworkCore;
using RBAC.Data;
using RBAC.Models;
using RBAC.Repositories.Interfaces;

namespace RBAC.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role?> GetByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(x => x.RoleName == roleName);
        }

        public async Task<Role> AddAsync(Role role)
        {
            _context.Roles.Add(role);

            await _context.SaveChangesAsync();

            return role;
        }

        public async Task<Role> UpdateAsync(Role role)
        {
            _context.Roles.Update(role);

            await _context.SaveChangesAsync();

            return role;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
                return false;

            _context.Roles.Remove(role);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AssignPermissionAsync(int roleId, int permissionId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            var permission = await _context.Permissions.FindAsync(permissionId);

            if (role == null || permission == null)
                return false;

            bool exists = await _context.RolePermissions.AnyAsync(x => x.RoleId == roleId && x.PermissionId == permissionId);

            if (exists)
                return false;

            var rolePermission = new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId
            };

            _context.RolePermissions.Add(rolePermission);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AssignRoleToUserAsync(int userId, int roleId)
        {
            var user = await _context.Users.FindAsync(userId);
            var role = await _context.Roles.FindAsync(roleId);

            if (user == null || role == null)
                return false;

            bool exists = await _context.UserRoles.AnyAsync(x => x.UserId == userId && x.RoleId == roleId);

            if (exists)
                return false;

            _context.UserRoles.Add(new UserRole
            {
                UserId = userId,
                RoleId = roleId
            });

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
