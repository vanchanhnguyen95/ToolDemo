﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpeedWebAPI.Infrastructure;

namespace SpeedWebAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SpeedWebAPI.Models.SpeedLimit", b =>
                {
                    b.Property<decimal>("Lat")
                        .HasPrecision(18, 10)
                        .HasColumnType("decimal(18,10)");

                    b.Property<decimal>("Lng")
                        .HasPrecision(18, 10)
                        .HasColumnType("decimal(18,10)");

                    b.Property<int?>("ProviderType")
                        .HasMaxLength(1)
                        .HasColumnType("int");

                    b.Property<string>("Position")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleteFlag")
                        .HasColumnType("int");

                    b.Property<int?>("MaxSpeed")
                        .HasColumnType("int");

                    b.Property<int?>("MinSpeed")
                        .HasColumnType("int");

                    b.Property<bool?>("PointError")
                        .HasColumnType("bit");

                    b.Property<long?>("SegmentID")
                        .HasColumnType("bigint");

                    b.Property<int?>("UpdateCount")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Lat", "Lng", "ProviderType", "Position");

                    b.ToTable("SpeedLimit");
                });
#pragma warning restore 612, 618
        }
    }
}
