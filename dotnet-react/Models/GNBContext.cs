using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace dotnet_react.Models
{
    public partial class GNBContext : DbContext
    {
        public GNBContext()
        {
        }

        public GNBContext(DbContextOptions<GNBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Rates> Rates { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(local);Database=GNB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<Rates>(entity =>
            {
                entity.ToTable("rates");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FromCurr).HasMaxLength(10);

                entity.Property(e => e.Rate)
                    .HasColumnName("rate")
                    .HasColumnType("decimal(16, 8)");

                entity.Property(e => e.ToCurr).HasMaxLength(10);
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.ToTable("transactions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(16, 8)");

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(10);

                entity.Property(e => e.Sku)
                    .HasColumnName("sku")
                    .HasMaxLength(10);
            });
        }
    }
}
