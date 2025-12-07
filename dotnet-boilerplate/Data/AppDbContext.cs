using dotnet_boilerplate.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_boilerplate.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Role> Roles { get; set; }
    }
}
