using RoleBase.DTOs;
using RoleBase.Repositories.Interface;
using RoleBase.Services;
using RoleBase.Model;
using Microsoft.AspNetCore.Identity;
using RoleBase.Data;
using RoleBase.Model;

namespace RoleBase.Repositories.RepoImplementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        private readonly ApplicationDbContext _context;
        private readonly IRoleRepository _roleRepository;

        public AuthRepository(IUserRepository userRepository, JwtService jwtService , ApplicationDbContext context , IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _context = context;
            _roleRepository = roleRepository;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                return false;
            }
            var hashPassword =  BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = hashPassword
            };
            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();

            var UserRoles = _context.Roles.FirstOrDefault(x => x.RoleName == "Student");

            if (UserRoles != null) 
            {
                var assignRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = UserRoles.Id
                };
              _context.UserRoles.Add(assignRole);
              await _userRepository.SaveAsync();
            }
            return true;

        }

        public async Task<string?> LoginAsync(LoginDto dto) 
        {
            var user = await _userRepository.GetUserByEmailAsync(dto.Email);

            if (user == null) { return null; }
            bool isValid;
             if (user.Id == 2 && user.Id ==  3)
             {
                 isValid = dto.Password == user.Password;
             }
             else 
             {
                 isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
             }
            

            //bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);

            if (!isValid) { return null; }
            
            var RoleName = await  _roleRepository.GetUserRoleAsync(user.Id);
            
            RoleName ??= "User";

            return _jwtService.GenerateToken(user , RoleName);
        }
    }
}