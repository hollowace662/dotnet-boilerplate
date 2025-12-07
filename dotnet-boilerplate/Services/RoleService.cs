using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;
using dotnet_boilerplate.Repositories;

namespace dotnet_boilerplate.Services
{
    public class RoleService(IRoleRepository repository) : IRoleService
    {
        private readonly IRoleRepository _repository = repository;

        //Create
        public async Task<Role> CreateRoleAsync(CreateRoleDTO createRoleDTO)
        {
            return await _repository.CreateRoleAsync(createRoleDTO);
        }

        //Read
        public async Task<Role?> GetRoleByIdAsync(int id)
        {
            return await _repository.GetRoleByIdAsync(id);
        }

        public Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return _repository.GetAllRolesAsync();
        }

        //Update
        public Task<Role?> UpdateRoleAsync(int id, UpdateRoleDTO updateRoleDTO)
        {
            return _repository.UpdateRoleAsync(id, updateRoleDTO);
        }

        //Delete
        public Task<bool> DeleteRoleAsync(int id)
        {
            return _repository.DeleteRoleAsync(id);
        }
    }
}
