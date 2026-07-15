using RBAC.DTOs;

namespace RBAC.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> Register(RegisterDto dto);

        Task<string?> Login(LoginDto dto);
    }
}
