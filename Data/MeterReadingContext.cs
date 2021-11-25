using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Ensek.Models;

namespace Ensek.Data
{
    public class MeterReadingContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<MeterReading> MeterReadings { get; set; }
        public MeterReadingContext(DbContextOptions<MeterReadingContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=MeterReading;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountId);
                entity.Property(e => e.AccountId).ValueGeneratedNever();
                entity.ToTable("Account");
            });

            modelBuilder.Entity<MeterReading>(entity =>
            {
                entity.Ignore(m => m.Errors);
                entity.ToTable("MeterReading");
                entity.HasIndex(m => new { m.AccountId, m.MeterReadingDateTime }).IsUnique(true);
            });
        }
    }
}