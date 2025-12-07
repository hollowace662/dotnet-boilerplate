using dotnet_boilerplate.Data;
using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;

namespace dotnet_boilerplate.Repositories
{
    public class RoleRepository(AppDbContext context) : IRoleRepository
    {
        private readonly AppDbContext _context = context;

        //Create
        public async Task<Role> CreateRoleAsync(CreateRoleDTO createRoleDTO)
        {
            var createdRole = await _context.Roles.AddAsync(
                new Role { Name = createRoleDTO.Name, Description = createRoleDTO.Description }
            );
            await _context.SaveChangesAsync();
            return createdRole.Entity;
        }
    }
}
