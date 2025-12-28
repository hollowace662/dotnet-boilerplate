using garantisa.DTO;
using garantisa.Models;
using garantisa.Repositories;

namespace garantisa.Services
{
    public class RoleService(IRoleRepository repository) : IRoleService
    {
        private readonly IRoleRepository _repository = repository;

        public async Task<List<RoleDTO>> GetAllRolesAsync()
        {
            var roles = await _repository.GetAllRolesAsync();
            return
            [
                .. roles.Select(role => new RoleDTO
                {
                    RoleId = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                }),
            ];
        }

        public async Task<RoleDTO?> GetRoleByIdAsync(int id)
        {
            var role = await _repository.GetRoleByIdAsync(id);
            if (role == null)
            {
                return null;
            }
            return new RoleDTO
            {
                RoleId = role.Id,
                Name = role.Name,
                Description = role.Description,
            };
        }

        public async Task<RoleDTO> CreateRoleAsync(CreateRoleRequestDTO createRoleDTO)
        {
            var role = new Role
            {
                Name = createRoleDTO.Name!,
                Description = createRoleDTO.Description!,
            };
            var createdRole = await _repository.CreateRoleAsync(role);
            return new RoleDTO
            {
                RoleId = createdRole.Id,
                Name = createdRole.Name,
                Description = createdRole.Description,
            };
        }

        public async Task<RoleDTO?> UpdateRoleAsync(int id, UpdateRoleRequestDTO updateRoleDTO)
        {
            var role = await _repository.GetRoleByIdAsync(id);
            if (role == null)
            {
                return null;
            }
            if (updateRoleDTO.Name != null)
            {
                role.Name = updateRoleDTO.Name;
            }
            if (updateRoleDTO.Description != null)
            {
                role.Description = updateRoleDTO.Description;
            }
            var updatedRole = await _repository.UpdateRoleAsync(role);
            if (updatedRole == null)
            {
                return null;
            }
            return new RoleDTO
            {
                RoleId = updatedRole.Id,
                Name = updatedRole.Name,
                Description = updatedRole.Description,
            };
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            return await _repository.DeleteRoleAsync(id);
        }
    }
}
