using dotnet_boilerplate.Data;
using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;

namespace dotnet_boilerplate.Services
{
    public class RoleService(AppDbContext context) : IRoleService
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

        //Read
        public Task<Role?> GetRoleByIdAsync(GetRoleDTO getRoleDTO)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            throw new NotImplementedException();
        }

        //Update
        public Task<Role?> UpdateRoleAsync(UpdateRoleDTO updateRoleDTO)
        {
            throw new NotImplementedException();
        }

        //Delete
        public Task<bool> DeleteRoleAsync(DeleteRoleDTO deleteRoleDTO)
        {
            throw new NotImplementedException();
        }
    }
}
