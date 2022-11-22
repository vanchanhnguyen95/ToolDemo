using Microsoft.EntityFrameworkCore;
using SpeedWebAPI.Models;

namespace SpeedWebAPI.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<SpeedLimit> SpeedLimits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpeedLimit>().HasKey(u => new
            {
                u.Lat,
                u.Lng
            });
        }
    }
}
