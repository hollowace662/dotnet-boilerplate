using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;
using dotnet_boilerplate.Repositories;

namespace dotnet_boilerplate.Services
{
    public class RoleService(IRoleRepository repository) : IRoleService
    {
        private readonly IRoleRepository _repository = repository;

        public Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return _repository.GetAllRolesAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(int id)
        {
            return await _repository.GetRoleByIdAsync(id);
        }

        public async Task<Role> CreateRoleAsync(CreateRoleRequestDTO createRoleDTO)
        {
            return await _repository.CreateRoleAsync(createRoleDTO);
        }

        public Task<Role?> UpdateRoleAsync(int id, UpdateRoleRequestDTO updateRoleDTO)
        {
            return _repository.UpdateRoleAsync(id, updateRoleDTO);
        }

        public Task<bool> DeleteRoleAsync(int id)
        {
            return _repository.DeleteRoleAsync(id);
        }
    }
}
