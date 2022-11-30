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
        public virtual DbSet<SpeedLimit3Point> SpeedLimit3Points { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpeedLimit>().HasKey(u => new
            {
                u.Lat,
                u.Lng,
                u.ProviderType
            });
            //modelBuilder.Entity<SpeedLimit>().Property(x => x.Lat).HasPrecision(10, 8);
            //modelBuilder.Entity<SpeedLimit>().Property(x => x.Lng).HasPrecision(10, 8);

            modelBuilder.Entity<SpeedLimit3Point>().HasKey(u => new
            {
                u.Lat,
                u.Lng,
                u.ProviderType
            });
            //modelBuilder.Entity<SpeedLimit3Point>().Property(x => x.Lat).HasPrecision(10, 8);
            //modelBuilder.Entity<SpeedLimit3Point>().Property(x => x.Lng).HasPrecision(10, 8);
        }
    }
}
