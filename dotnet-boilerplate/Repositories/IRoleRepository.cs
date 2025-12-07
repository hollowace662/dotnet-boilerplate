using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;

namespace dotnet_boilerplate.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> CreateRoleAsync(CreateRoleDTO createRoleDTO);
    }
}
