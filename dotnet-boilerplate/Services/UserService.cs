using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;
using dotnet_boilerplate.Repositories;

namespace dotnet_boilerplate.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(CreateUserDTO createUserDTO)
        {
            if (
                string.IsNullOrWhiteSpace(createUserDTO.Username)
                || string.IsNullOrWhiteSpace(createUserDTO.Email)
            )
            {
                throw new ArgumentException("Username and Email cannot be empty.");
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
    }
}
