using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;

namespace dotnet_boilerplate.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role?> GetRoleByIdAsync(int id);
        Task<Role> CreateRoleAsync(CreateRoleRequestDTO createRoleDTO);
        Task<Role?> UpdateRoleAsync(int id, UpdateRoleRequestDTO updateRoleDTO);
        Task<bool> DeleteRoleAsync(int id);
    }
}
