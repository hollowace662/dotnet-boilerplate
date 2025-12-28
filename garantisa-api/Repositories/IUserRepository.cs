using garantisa.Models;

namespace garantisa.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user, List<int> roleIds);
        Task<User> UpdateUserAsync(User user, List<int> roleIds);
        Task<bool> DeleteUserAsync(int id);
        Task<List<int>> GetUserRolesAsync(int id);
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);
    }
}
