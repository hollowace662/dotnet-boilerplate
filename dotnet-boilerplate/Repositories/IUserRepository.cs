using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;

namespace dotnet_boilerplate.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>?> GetAllUsersAsync();
        Task<GetUserResponseDTO?> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(CreateUserRequestDTO createUserDTO);
        Task<UpdateUserResponseDTO?> UpdateUserAsync(int id, UpdateUserRequestDTO updateUserDTO);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);
    }
}
