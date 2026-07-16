using RoleBase.Model;

namespace RoleBase.Repositories.Interface
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync();
        Task<IEnumerable<Permission>> GetByIdAsync(int id);
        Task<Permission> GetByNameAsync(string name);
        Task AddAsync(Permission permission);
        Task<List<Permission>> GetPermissionsByUserIdAsync(int userId);
        Task DeleteAsync(Permission permission);
        Task AssignPermissionToRoleAsync(int UserId, int PermissionId);

        Task SaveAsync();
    }
}
