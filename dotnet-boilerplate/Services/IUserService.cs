using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;

namespace dotnet_boilerplate.Services
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(CreateUserDTO createUserDTO);
    }
}
