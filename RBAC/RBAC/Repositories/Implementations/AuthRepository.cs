using Microsoft.EntityFrameworkCore;
using RBAC.Data;
using RBAC.DTOs;
using RBAC.Repositories.Interfaces;
using RBAC.Services;
using RBAC.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace RBAC.Repositories.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;
        private readonly IUserRepository _userRepository;

        public AuthRepository(
            AppDbContext context,
            JwtService jwtService,
            IUserRepository userRepository)
        {
            _context = context;
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        public async Task<bool> Register(RegisterDto dto)
        {
            bool exists = await _context.Users.AnyAsync(x => x.Email == dto.Email);
            if (exists)
                return false;


            User user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreateDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                PhoneNumber = dto.PhoneNumber,
                City = dto.City,
                State = dto.State,
                Country = dto.Country,
                IsDeleted = false,
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            int userId = user.Id;

            var role = await _userRepository.GetRoleByNameAsync("User");

            if (role == null)
                return false;

            await _userRepository.AssignRoleToUserAsync(userId, role.Id);

            return true;
        }

        public async Task<string?> Login (LoginDto dto)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return null;

            return _jwtService.GenerateToken(user);
        }
    }
}
