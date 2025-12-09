using dotnet_boilerplate.Data;
using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_boilerplate.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<List<User>?> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<GetUserResponseDTO?> GetUserByIdAsync(int id)
        {
            var user =
                await _context.Users.FindAsync(id)
                ?? throw new ArgumentException($"User with ID {id} does not exist.");
            var userRoles = await _context.UserRoles.Where(ur => ur.UserId == id).ToListAsync();
            return new GetUserResponseDTO
            {
                User = user,
                Roles = [.. userRoles.Select(ur => ur.RoleId)],
            };
        }

        public async Task<User> CreateUserAsync(CreateUserRequestDTO createUserDTO)
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            var user = new User
            {
                Username = createUserDTO.Username!.ToLowerInvariant(),
                Email = createUserDTO.Email!.ToLowerInvariant(),
            };
            _context.Users.Add(user);

            await _context.SaveChangesAsync();
            foreach (var roleId in createUserDTO.RoleIds!)
            {
                var role = await _context.Roles.FindAsync(roleId);
                if (role == null)
                {
                    await tx.RollbackAsync();
                    throw new ArgumentException($"Role with ID {roleId} does not exist.");
                }

                _context.UserRoles.Add(new UsersRoles { UserId = user.Id, RoleId = roleId });
            }
            await _context.SaveChangesAsync();
            await tx.CommitAsync();
            return user;
        }

        public async Task<UpdateUserResponseDTO?> UpdateUserAsync(
            int id,
            UpdateUserRequestDTO updateUserDTO
        )
        {
            var user =
                await _context.Users.FindAsync(id)
                ?? throw new ArgumentException($"User with ID {id} does not exist.");
            if (!string.IsNullOrWhiteSpace(updateUserDTO.Username))
            {
                user.Username = updateUserDTO.Username.ToLowerInvariant();
            }

            if (!string.IsNullOrWhiteSpace(updateUserDTO.Email))
            {
                user.Email = updateUserDTO.Email.ToLowerInvariant();
            }

            using var tx = await _context.Database.BeginTransactionAsync();

            _context.Users.Update(user);

            var userRoles = await _context.UserRoles.Where(ur => ur.UserId == id).ToListAsync();
            _context.UserRoles.RemoveRange(userRoles);

            foreach (var roleId in updateUserDTO.RoleIds!)
            {
                var role = await _context.Roles.FindAsync(roleId);
                if (role == null)
                {
                    await tx.RollbackAsync();
                    throw new ArgumentException($"Role with ID {roleId} does not exist.");
                }

                _context.UserRoles.Add(new UsersRoles { UserId = user.Id, RoleId = roleId });
            }

            await _context.SaveChangesAsync();
            await tx.CommitAsync();

            return new UpdateUserResponseDTO { User = user, RoleIds = updateUserDTO.RoleIds };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            var normalized = username.ToLowerInvariant();
            return await _context.Users.AnyAsync(u => u.Username.ToLower() == normalized);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var normalized = email.ToLowerInvariant();
            return await _context.Users.AnyAsync(u => u.Email.ToLower() == normalized);
        }
    }
}
