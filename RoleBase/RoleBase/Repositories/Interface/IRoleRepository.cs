using RoleBase.Model;

namespace RoleBase.Repositories.Interface
{
    public interface IRoleRepository
    {

        Task<IEnumerable<Role>> GetAllAsync();

        Task<Role?> GetByIdAsync(int id);

        Task<Role?> GetByNameAsync(string roleName);

        Task AddAsync(Role role);

        Task DeleteAsync(Role role);

        Task AssignRoleToUserAsync(int userId, int roleId);

        Task<string?> GetUserRoleAsync(int userId);

        Task SaveAsync();

    }
}
