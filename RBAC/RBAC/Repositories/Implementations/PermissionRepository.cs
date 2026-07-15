using Microsoft.EntityFrameworkCore;
using RBAC.Data;
using RBAC.DTOs;
using RBAC.Models;
using RBAC.Repositories.Interfaces;

namespace RBAC.Repositories.Implementations
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AppDbContext _context;

        public PermissionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission?> GetByIdAsync(int id)
        {
            return await _context.Permissions.FindAsync(id);
        }

        public async Task<Permission?> GetByNameAsync(string permissionName)
        {
            return await _context.Permissions.FirstOrDefaultAsync(x => x.PermissionName == permissionName);
        }

        public async Task<Permission> AddAsync(Permission permission)
        {
            _context.Permissions.Add(permission);

            await _context.SaveChangesAsync();

            return permission;
        }

        public async Task<Permission?> UpdateAsync(int id, CreatePermissionDto dto)
        {
            var existing = await _context.Permissions.FindAsync(id);

            if (existing == null)
                return null;

            bool exists = await _context.Permissions
                .AnyAsync(p => p.PermissionName == dto.PermissionName && p.Id != id);

            if (exists)
                throw new Exception("Permission already exists");

            existing.PermissionName = dto.PermissionName;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);

            if (permission == null)
                return false;

            _context.Permissions.Remove(permission);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
