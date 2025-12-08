using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;

namespace dotnet_boilerplate.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(CreateUserDTO createUserDTO);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> UsernameExistsAsync(string username);
    }
}
