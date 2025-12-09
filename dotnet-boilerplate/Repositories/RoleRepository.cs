using dotnet_boilerplate.Data;
using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_boilerplate.Repositories
{
    public class RoleRepository(AppDbContext context) : IRoleRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role> CreateRoleAsync(CreateRoleRequestDTO createRoleDTO)
        {
            var createdRole = await _context.Roles.AddAsync(
                new Role { Name = createRoleDTO.Name, Description = createRoleDTO.Description }
            );
            await _context.SaveChangesAsync();
            return createdRole.Entity;
        }

        public async Task<Role?> UpdateRoleAsync(int id, UpdateRoleRequestDTO updateRoleDTO)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return null;
            }
            if (updateRoleDTO.Name != null)
            {
                role.Name = updateRoleDTO.Name;
            }
            if (updateRoleDTO.Description != null)
            {
                role.Description = updateRoleDTO.Description;
            }
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
