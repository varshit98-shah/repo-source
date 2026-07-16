using RoleBase.DTOs;
using RoleBase.Model;

namespace RoleBase.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(int userId);
        Task<User?> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task<bool> DeleteMultipleAsync(List<int> userIds);
        Task AddAsync(User user);
        Task CreateAsync(RegisterDto user);
        Task<bool> UpdateAsync(UpdateDto user);
        Task<bool> DeleteAsync(int userId ,int LoggedId);
        Task SaveAsync();


    }
}
