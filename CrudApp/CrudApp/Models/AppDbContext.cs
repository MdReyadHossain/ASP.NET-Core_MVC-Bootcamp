using CrudApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrudApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
