using RBAC.DTOs;
using RBAC.Models;

namespace RBAC.Repositories.Interfaces
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync();

        Task<Permission?> GetByIdAsync(int id);

        Task<Permission?> GetByNameAsync(string permissionName);

        Task<Permission> AddAsync(Permission permission);

        Task<Permission> UpdateAsync(int id, CreatePermissionDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
