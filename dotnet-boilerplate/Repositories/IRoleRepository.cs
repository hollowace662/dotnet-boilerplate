using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;

namespace dotnet_boilerplate.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> CreateRoleAsync(CreateRoleDTO createRoleDTO);
        Task<Role?> GetRoleByIdAsync(int id);
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role?> UpdateRoleAsync(int id, UpdateRoleDTO updateRoleDTO);
        Task<bool> DeleteRoleAsync(int id);
    }
}
