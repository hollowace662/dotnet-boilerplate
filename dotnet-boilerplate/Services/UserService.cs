using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;
using dotnet_boilerplate.Repositories;

namespace dotnet_boilerplate.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<List<UserDTO>?> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users
                .Select(u => new UserDTO
                {
                    UserId = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                })
                .ToList();
        }

        public async Task<UserWithRolesDTO?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            var roleIds = await _userRepository.GetUserRolesAsync(id);
            return new UserWithRolesDTO
            {
                User = new UserDTO
                {
                    UserId = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                },
                RoleIds = roleIds,
            };
        }

        public async Task<UserWithRolesDTO> CreateUserAsync(CreateUserRequestDTO createUserDTO)
        {
            if (await _userRepository.UsernameExistsAsync(createUserDTO.Username!))
            {
                throw new ArgumentException("Username already exists.");
            }

            if (await _userRepository.EmailExistsAsync(createUserDTO.Email!))
            {
                throw new ArgumentException("Email already exists.");
            }

            var user = await _userRepository.CreateUserAsync(
                user: new User { Username = createUserDTO.Username!, Email = createUserDTO.Email! },
                roleIds: createUserDTO.RoleIds ?? []
            );

            var roles = await _userRepository.GetUserRolesAsync(user.Id);

            return new UserWithRolesDTO
            {
                User = new UserDTO
                {
                    UserId = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                },
                RoleIds = roles,
            };
        }

        public async Task<UserWithRolesDTO> UpdateUserAsync(
            int id,
            UpdateUserRequestDTO updateUserDTO
        )
        {
            var user =
                await _userRepository.GetUserByIdAsync(id)
                ?? throw new ArgumentException("User not found.");

            if (
                updateUserDTO.Username != null
                && updateUserDTO.Username != user.Username
                && await _userRepository.UsernameExistsAsync(updateUserDTO.Username!)
            )
            {
                throw new ArgumentException("Username already exists.");
            }
            if (
                updateUserDTO.Email != null
                && updateUserDTO.Email != user.Email
                && await _userRepository.EmailExistsAsync(updateUserDTO.Email!)
            )
            {
                throw new ArgumentException("Email already exists.");
            }

            user.Username = updateUserDTO.Username ?? user.Username;
            user.Email = updateUserDTO.Email ?? user.Email;

            var roles = await _userRepository.GetUserRolesAsync(id);

            var updatedUser = await _userRepository.UpdateUserAsync(
                user: user,
                roleIds: updateUserDTO.RoleIds ?? roles
            );
            return new UserWithRolesDTO
            {
                User = new UserDTO
                {
                    UserId = updatedUser.Id,
                    Username = updatedUser.Username,
                    Email = updatedUser.Email,
                },
                RoleIds = await _userRepository.GetUserRolesAsync(updatedUser.Id),
            };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }
    }
}
