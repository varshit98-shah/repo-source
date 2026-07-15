using RBAC.Models;

namespace RBAC.Repositories.Implementations
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync();

        Task<Role?> GetByIdAsync(int id);

        Task<Role?> GetByNameAsync(string roleName);

        Task<Role> AddAsync(Role role);

        Task<Role> UpdateAsync(Role role);

        Task<bool> DeleteAsync(int id);

        Task<bool> AssignPermissionAsync(int roleId, int permissionId);
        Task<bool> AssignRoleToUserAsync(int userId, int roledId);
    }
}
