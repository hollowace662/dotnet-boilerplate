using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;

namespace dotnet_boilerplate.Services
{
    public interface IRoleService
    {
        //Create
        public Task<Role> CreateRoleAsync(CreateRoleDTO createRoleDTO);

        //Read
        public Task<Role?> GetRoleByIdAsync(int id);
        public Task<IEnumerable<Role>> GetAllRolesAsync();

        //Update
        public Task<Role?> UpdateRoleAsync(int id, UpdateRoleDTO updateRoleDTO);

        //Delete
        public Task<bool> DeleteRoleAsync(int id);
    }
}
