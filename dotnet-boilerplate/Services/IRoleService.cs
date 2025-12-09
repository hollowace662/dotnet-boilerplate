using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;

namespace dotnet_boilerplate.Services
{
    public interface IRoleService
    {
        public Task<IEnumerable<Role>> GetAllRolesAsync();
        public Task<Role?> GetRoleByIdAsync(int id);
        public Task<Role> CreateRoleAsync(CreateRoleRequestDTO createRoleDTO);
        public Task<Role?> UpdateRoleAsync(int id, UpdateRoleRequestDTO updateRoleDTO);
        public Task<bool> DeleteRoleAsync(int id);
    }
}
