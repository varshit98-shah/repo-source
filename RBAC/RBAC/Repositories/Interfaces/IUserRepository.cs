using RBAC.DTOs;
using RBAC.Models;

namespace RBAC.Repositories.Implementations
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User?> GetByIdAsync(int id);

        Task<User?> GetByEmailAsync(string email);

        Task<Role?> GetRoleByNameAsync(string roleName);

        Task<List<string>> GetUserPermissionAsync(int userId);

        Task<bool> AssignRoleToUserAsync(int userId, int roleId);

        Task<User> AddAsync(RegisterDto dto);

        Task<User> UpdateAsync(int id, UpdateUserDto dto, string? modifiedBy);

        Task<bool> DeleteAsync(int id);
    }
}
