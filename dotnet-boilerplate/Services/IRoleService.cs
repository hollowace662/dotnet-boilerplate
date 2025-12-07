using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;

namespace dotnet_boilerplate.Services
{
    public interface IRoleService
    {
        //Create
        public Task<Role> CreateRoleAsync(CreateRoleDTO createRoleDTO);

        //Read
        public Task<Role?> GetRoleByIdAsync(GetRoleDTO getRoleDTO);
        public Task<IEnumerable<Role>> GetAllRolesAsync();

        //Update
        public Task<Role?> UpdateRoleAsync(UpdateRoleDTO updateRoleDTO);

        //Delete
        public Task<bool> DeleteRoleAsync(DeleteRoleDTO deleteRoleDTO);
    }
}
