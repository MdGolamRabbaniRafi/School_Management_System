using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class DBContext : DbContext
    {
        // Parameterless constructor
        public DBContext()
        {
        }

        // Constructor with options (for DI or tests)
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        // Configure connection string here (replace with your actual connection string)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-RRK2KA0;Database=SchoolManagementSystem;Integrated Security=True;TrustServerCertificate=True;");
            }
        }
    }
}
