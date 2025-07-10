using DAL.Models;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

namespace DAL.EF
{
    public class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Load .env variables
                Env.Load();

                // Get connection string from environment variable
                var connectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");

                if (!string.IsNullOrEmpty(connectionString))
                {
                    optionsBuilder.UseNpgsql(connectionString);
                }
                else
                {
                    throw new InvalidOperationException("Connection string 'DEFAULT_CONNECTION' not found in environment variables.");
                }
            }
        }
    }
}
