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
