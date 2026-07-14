using RoleBase.DTOs;

namespace RoleBase.Repositories.Interface
{
    public interface IAuthRepository
    {
        Task<bool> RegisterAsync(RegisterDto dto);

        Task<string?> LoginAsync(LoginDto dto);
    }
}
