using Microsoft.EntityFrameworkCore;
using RoleBase.Data;
using RoleBase.Repositories.Interface;
using RoleBase.Model;

namespace RoleBase.Repositories.RepoImplementations
{
    public class ApiPermissions : IApiPermission
    {
        private readonly ApplicationDbContext _context;

        public ApiPermissions(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<string>> GetUserPermissionsAsync(int userId) 
        {
            return await _context.UserRoles
                                 .Where(ur => ur.UserId == userId)
                                 .SelectMany(ur => ur.Role.RolePermission)
                                 .Select(ur => ur.Permission.PermissionName)
                                 .ToListAsync();
        }
        public async Task<ApiPermission?> GetApiPermissionAsync(string methad, string path) 
        {
            return await _context.ApiPermissions
                .Include(x => x.Permission)
                .FirstOrDefaultAsync(x => x.HttpMethod == methad &&
                                            x.ApiPath == path);
        }

    }
}
