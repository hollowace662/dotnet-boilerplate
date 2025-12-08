using dotnet_boilerplate.Data;
using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_boilerplate.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<User> CreateUserAsync(CreateUserDTO createUserDTO)
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

        public async Task<bool> EmailExistsAsync(string email)
        {
            var normalized = email.ToLowerInvariant();
            return await _context.Users.AnyAsync(u => u.Email.ToLower() == normalized);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            var normalized = username.ToLowerInvariant();
            return await _context.Users.AnyAsync(u => u.Username.ToLower() == normalized);
        }
    }
}
