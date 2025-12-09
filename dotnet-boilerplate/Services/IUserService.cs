using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;

namespace dotnet_boilerplate.Services
{
    public interface IUserService
    {
        Task<List<User>?> GetAllUsersAsync();
        Task<GetUserResponseDTO?> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(CreateUserRequestDTO createUserDTO);
        Task<UpdateUserResponseDTO?> UpdateUserAsync(int id, UpdateUserRequestDTO updateUserDTO);
        Task<bool> DeleteUserAsync(int id);
    }
}
