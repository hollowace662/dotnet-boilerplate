using garantisa.DTO;

namespace garantisa.Services
{
    public interface IUserService
    {
        Task<List<UserDTO>?> GetAllUsersAsync();
        Task<UserWithRolesDTO?> GetUserByIdAsync(int id);
        Task<UserWithRolesDTO> CreateUserAsync(CreateUserRequestDTO createUserDTO);
        Task<UserWithRolesDTO> UpdateUserAsync(int id, UpdateUserRequestDTO updateUserDTO);
        Task<bool> DeleteUserAsync(int id);
    }
}
