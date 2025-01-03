using Microsoft.EntityFrameworkCore;
using Starcross.Models;  // Add this reference to your Models folder

namespace Starcross.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => u.user_id); // Define the primary key here if using a custom name
        }

        // Define your DbSets (models) here, for example:
        public DbSet<User> Users { get; set; }
    }
}
