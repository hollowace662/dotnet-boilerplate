using dotnet_boilerplate.Data;
using dotnet_boilerplate.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_boilerplate.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user =
                await _context.Users.FindAsync(id)
                ?? throw new ArgumentException($"User with ID {id} does not exist.");
            return user;
        }

        public async Task<User> CreateUserAsync(User user, List<int> roleIds)
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            user.Username = user.Username!.ToLowerInvariant();
            user.Email = user.Email!.ToLowerInvariant();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            foreach (var roleId in roleIds!)
            {
                var role = await _context.Roles.FindAsync(roleId);
                if (role == null)
                {
                    await tx.RollbackAsync();
                    throw new ArgumentException($"Role with ID {roleId} does not exist.");
                }
                _context.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = roleId });
            }

            await _context.SaveChangesAsync();
            await tx.CommitAsync();

            return user;
        }

        public async Task<User> UpdateUserAsync(User user, List<int> roleIds)
        {
            if (!string.IsNullOrWhiteSpace(user.Username))
            {
                user.Username = user.Username.ToLowerInvariant();
            }

            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                user.Email = user.Email.ToLowerInvariant();
            }

            using var tx = await _context.Database.BeginTransactionAsync();

            _context.Users.Update(user);

            var userRoles = await _context
                .UserRoles.Where(ur => ur.UserId == user.Id)
                .ToListAsync();
            _context.UserRoles.RemoveRange(userRoles);

            foreach (var roleId in roleIds!)
            {
                var role = await _context.Roles.FindAsync(roleId);
                if (role == null)
                {
                    await tx.RollbackAsync();
                    throw new ArgumentException($"Role with ID {roleId} does not exist.");
                }

                _context.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = roleId });
            }

            await _context.SaveChangesAsync();
            await tx.CommitAsync();
            return user;
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

        public async Task<List<int>> GetUserRolesAsync(int id)
        {
            var roles = await _context
                .UserRoles.Where(ur => ur.UserId == id)
                .Select(ur => ur.RoleId)
                .ToListAsync();
            return roles;
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
