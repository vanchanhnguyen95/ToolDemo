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
                u.Lng,
                u.ProviderType,
                u.Position
            });
            //modelBuilder.Entity<SpeedLimit>().Property(x => x.Lat).HasPrecision(10, 8);
            //modelBuilder.Entity<SpeedLimit>().Property(x => x.Lng).HasPrecision(11, 8);
        }
    }
}
