using dotnet_boilerplate.Data;
using dotnet_boilerplate.DTO;
using dotnet_boilerplate.Models;

namespace dotnet_boilerplate.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
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

            using var tx = await _context.Database.BeginTransactionAsync();

            var user = new User { Username = createUserDTO.Username, Email = createUserDTO.Email };
            _context.Users.Add(user);

            await _context.SaveChangesAsync();
            foreach (var roleId in createUserDTO.RoleIds)
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
    }
}
