using RoleBase.Model;

namespace RoleBase.Repositories.Interface
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync();
        Task<IEnumerable<Permission>> GetByIdAsync(int id);
        Task<Permission> GetByNameAsync(string name);
        Task AddAsync(Permission permission);
        Task DeleteAsync(Permission permission);
        Task AssignPermissionToRoleAsync(int UserId, int PermissionId);
        Task<List<string>> GetPermissionsByUserIdAsync(int UserId);
        Task SaveAsync();
    }
}
