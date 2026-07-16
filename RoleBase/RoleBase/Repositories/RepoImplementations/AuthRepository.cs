using RoleBase.DTOs;
using RoleBase.Repositories.Interface;
using RoleBase.Services;
using RoleBase.Model;
using RoleBase.Data;

namespace RoleBase.Repositories.RepoImplementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        private readonly ApplicationDbContext _context;
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;

        public AuthRepository(
            IUserRepository userRepository,
            JwtService jwtService,
            ApplicationDbContext context,
            IRoleRepository roleRepository,
            IPermissionRepository permissionRepository)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _context = context;
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var existingUser =
                await _userRepository
                    .GetUserByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                return false;
            }

            var hashedPassword =
                BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = hashedPassword
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();

            var defaultRole = _context.Roles
                .FirstOrDefault(r => r.RoleName == "Student");

            if (defaultRole != null)
            {
                var userRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = defaultRole.Id
                };

                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<string?> LoginAsync(LoginDto dto)
        {
            var user =
                await _userRepository
                    .GetUserByEmailAsync(dto.Email);

            if (user == null)
            {
                return null;
            }

            bool isValid;

            
             isValid = BCrypt.Net.BCrypt.Verify(
                    dto.Password,
                    user.Password);
            

            if (!isValid)
            {
                return null;
            }

            var roleName =
                await _roleRepository
                    .GetUserRoleAsync(user.Id);

            roleName ??= "User";

            var permissions =
                await _permissionRepository
                    .GetPermissionsByUserIdAsync(user.Id);

            var token =
                _jwtService.GenerateToken(
                    user,
                    roleName,
                    permissions);

            return token;
        }
    }
}