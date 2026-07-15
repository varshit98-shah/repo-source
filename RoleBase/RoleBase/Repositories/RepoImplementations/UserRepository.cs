using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RoleBase.Data;
using RoleBase.Model;
using RoleBase.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using RoleBase.DTOs;
using System.Net.WebSockets;

namespace RoleBase.Repositories.RepoImplementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /*
        Task<User?> GetUserByIdAsync(int userId);
        Task<User?> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();

        Task AddAsync(User user);
        Task SaveAsync();
        */
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task  AddAsync(User user) 
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
                Password = hashPassword,
                Email = dto.Email
            };
             _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var UserRole = _context.Roles.FirstOrDefault(x => x.RoleName == "Student");

            if (UserRole != null) 
            {
                var assignRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = UserRole.Id
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
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if(user == null) 
            {
                return false;
            };
            user.Name = dto.Name;
            user.Email = dto.Email;

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}