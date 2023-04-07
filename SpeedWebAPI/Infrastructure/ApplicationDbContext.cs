﻿using Microsoft.EntityFrameworkCore;
using SpeedWebAPI.Models;
using SpeedWebAPI.Models.SpeedLimitPQA;

namespace SpeedWebAPI.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<SpeedLimit> SpeedLimits { get; set; }

        public virtual DbSet<SpeedLimitPQA> SpeedLimitPQAs { get; set; }

        //public virtual DbSet<SpeedLimitPQA> GetSpeedLimitPQAs()
        //{
        //    return speedLimitPQAs;
        //}

        //public virtual void SetSpeedLimitPQAs(DbSet<SpeedLimitPQA> value)
        //{
        //    speedLimitPQAs = value;
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpeedLimit>().HasKey(u => new
            {
                u.Lat,
                u.Lng,
                u.ProviderType,
                u.Position
            });
            modelBuilder.Entity<SpeedLimit>().Property(x => x.Lat).HasColumnType("decimal(18,10)").HasPrecision(18,10).IsRequired(true);
            modelBuilder.Entity<SpeedLimit>().Property(x => x.Lng).HasColumnType("decimal(18,10)").HasPrecision(18,10).IsRequired(true);
            //modelBuilder.Entity<SpeedLimit>().Property(x => x.Lat).HasPrecision(10, 8);
            //modelBuilder.Entity<SpeedLimit>().Property(x => x.Lng).HasPrecision(11, 8);

            modelBuilder.Entity<SpeedLimitPQA>().HasKey(u => new
            {
                u.Lat,
                u.Lng,
                //u.ProviderType,
                //u.Position
            });
            modelBuilder.Entity<SpeedLimitPQA>().Property(x => x.Lat).HasColumnType("decimal(18,10)").HasPrecision(18, 10).IsRequired(true);
            modelBuilder.Entity<SpeedLimitPQA>().Property(x => x.Lng).HasColumnType("decimal(18,10)").HasPrecision(18, 10).IsRequired(true);
        }
    }
}
