using Microsoft.EntityFrameworkCore;
using Starcross.Models;  // Add this reference to your Models folder

namespace Starcross.Data
{
    public class AppDbContext : DbContext
    {
        // Create a set of users, For testing
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set Primary key to user_id
            modelBuilder.Entity<User>()
                .HasKey(u => u.user_id);
        }
    }
}
