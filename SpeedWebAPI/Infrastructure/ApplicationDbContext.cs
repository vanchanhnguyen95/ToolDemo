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
    }
}
