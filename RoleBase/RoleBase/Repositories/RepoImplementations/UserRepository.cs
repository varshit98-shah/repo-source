using Microsoft.EntityFrameworkCore;
using RoleBase.Data;
using RoleBase.DTOs;
using RoleBase.Model;
using RoleBase.Repositories.Interface;
using System.Security.Claims;
using System.Numerics;
using RoleBase.Middleware;
namespace RoleBase.Repositories.RepoImplementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context  )
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Id == userId &&
                    !x.IsDelete);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Email == email &&
                    !x.IsDelete);
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            return await _context.Users
                     .Where(x => !x.IsDelete)
                     .Select(x => new UserResponseDto
                            {
                              Id = x.Id,
                              Name = x.Name,
                              Email = x.Email
                            })
                     .ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(RegisterDto dto)
        {
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = hashPassword,
                IsDelete = false
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            // Default Role Assignment
            var defaultRole = await _context.Roles
                .FirstOrDefaultAsync(x => x.RoleName == "User");

            if (defaultRole != null)
            {
                var assignRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = defaultRole.Id
                };

                _context.UserRoles.Add(assignRole);

                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(UpdateDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Email == dto.Email &&
                    !x.IsDelete);

            if (user == null)
            {
                return false;
            }

            user.Name = dto.Name;
            user.Email = dto.Email;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id , int LoggedId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    !x.IsDelete);

            if (user == null)
            {
                return false;
            }
            if (id == LoggedId) 
            {
                return false;
            }
            user.IsDelete = true;

            await _context.SaveChangesAsync(); 

            return true;
        }

        public async Task<bool> DeleteMultipleAsync(List<int> userIds)
        {
            var users = await _context.Users
                .Where(x => userIds.Contains(x.Id) && !x.IsDelete)
                .ToListAsync();

            if (!users.Any())
            {
                return false;
            }

            foreach (var user in users)
            {
                user.IsDelete = true;
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}