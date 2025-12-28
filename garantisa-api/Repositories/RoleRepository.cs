using garantisa.Data;
using garantisa.Models;
using Microsoft.EntityFrameworkCore;

namespace garantisa.Repositories
{
    public class RoleRepository(AppDbContext context) : IRoleRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Role>> GetAllRolesAsync()
        {
            var roles = await _context.Roles.ToListAsync();
            return roles;
        }

        public async Task<Role?> GetRoleByIdAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return null;
            }
            return role;
        }

        public async Task<Role> CreateRoleAsync(Role role)
        {
            var createdRole = await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return createdRole.Entity;
        }

        public async Task<Role> UpdateRoleAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return false;
            }
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
