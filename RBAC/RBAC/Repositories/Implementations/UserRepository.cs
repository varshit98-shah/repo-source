using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RBAC.Data;
using RBAC.DTOs;
using RBAC.Models;

namespace RBAC.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(x => x.UserRoles)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<List<string>> GetUserPermissionAsync(int userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => rp.Permission.PermissionName)
                .Distinct()
                .ToListAsync();
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(u => u.RoleName == roleName);
        }

        public async Task<bool> AssignRoleToUserAsync(int userId, int roleId)
        {
            _context.UserRoles.Add(new UserRole
            {
                UserId = userId,
                RoleId = roleId,
            });
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User> AddAsync(RegisterDto dto)
        {
            bool exists = await _context.Users.AnyAsync(x => x.Email == dto.Email);
            if (exists)
                return null;

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                CreateDate = DateTime.UtcNow,
                CreatedBy = "Admin",
                ModifiedDate = DateTime.UtcNow,
                IsDeleted = false,
                PhoneNumber = dto.PhoneNumber,
                City = dto.City,
                State = dto.State,
                Country = dto.Country,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            int userId = user.Id;

            var role = await GetRoleByNameAsync("User");

            if (role == null)
                return null;

            await AssignRoleToUserAsync(userId, role.Id);

            return user;

        }

        public async Task<User> UpdateAsync(int id, UpdateUserDto dto,string? ModifiedBy)
        {
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
                return null;

            bool emailExists = await _context.Users
                .AnyAsync(u => u.Email == dto.Email && u.Id != id);

            if (emailExists)
                throw new Exception("Email already exists.");

            existingUser.Name = dto.Name;
            existingUser.Email = dto.Email;
            existingUser.ModifiedBy = ModifiedBy;
            existingUser.ModifiedDate = DateTime.UtcNow;

            if(!string.IsNullOrWhiteSpace(dto.Password))
            {
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            await _context.SaveChangesAsync();

            return existingUser;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return false;

            user.IsDeleted = true;
            user.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

