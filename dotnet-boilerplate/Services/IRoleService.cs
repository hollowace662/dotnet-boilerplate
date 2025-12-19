using dotnet_boilerplate.DTO;

namespace dotnet_boilerplate.Services
{
    public interface IRoleService
    {
        public Task<List<RoleDTO>> GetAllRolesAsync();
        public Task<RoleDTO?> GetRoleByIdAsync(int id);
        public Task<RoleDTO> CreateRoleAsync(CreateRoleRequestDTO createRoleDTO);
        public Task<RoleDTO?> UpdateRoleAsync(int id, UpdateRoleRequestDTO updateRoleDTO);
        public Task<bool> DeleteRoleAsync(int id);
    }
}
