using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;
using dotnet_boilerplate.Repositories;

namespace dotnet_boilerplate.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<List<User>?> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<GetUserResponseDTO?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User> CreateUserAsync(CreateUserRequestDTO createUserDTO)
        {
            if (
                string.IsNullOrWhiteSpace(createUserDTO.Username)
                || string.IsNullOrWhiteSpace(createUserDTO.Email)
            )
            {
                throw new ArgumentException("Username or Email cannot be empty.");
            }

            if (createUserDTO.RoleIds == null || createUserDTO.RoleIds.Count == 0)
            {
                throw new ArgumentException("At least one role must be assigned to the user.");
            }

            if (await _userRepository.UsernameExistsAsync(createUserDTO.Username))
            {
                throw new ArgumentException("Username already exists.");
            }

            if (await _userRepository.EmailExistsAsync(createUserDTO.Email))
            {
                throw new ArgumentException("Email already exists.");
            }

            var user = await _userRepository.CreateUserAsync(createUserDTO);
            return user;
        }

        public async Task<UpdateUserResponseDTO?> UpdateUserAsync(
            int id,
            UpdateUserRequestDTO updateUserDTO
        )
        {
            if (updateUserDTO.RoleIds != null && updateUserDTO.RoleIds.Count == 0)
            {
                throw new ArgumentException("At least one role must be assigned to the user.");
            }
            if (
                updateUserDTO.Username != null
                && await _userRepository.UsernameExistsAsync(updateUserDTO.Username!)
            )
            {
                throw new ArgumentException("Username already exists.");
            }
            if (
                updateUserDTO.Email != null
                && await _userRepository.EmailExistsAsync(updateUserDTO.Email!)
            )
            {
                throw new ArgumentException("Email already exists.");
            }
            return await _userRepository.UpdateUserAsync(id, updateUserDTO);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }
    }
}
