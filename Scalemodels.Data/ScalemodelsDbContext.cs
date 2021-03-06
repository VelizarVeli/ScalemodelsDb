﻿using Microsoft.EntityFrameworkCore;
using Scalemodels.Models;

namespace Scalemodels.Data
{
    public class ScalemodelsDbContext : DbContext
    {
        public ScalemodelsDbContext()
        {
        }

        public ScalemodelsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AvailableModel> Models { get; set; }
        public DbSet<CompletedModelShow> CompletedModelShows { get; set; }
        public DbSet<ModelShow> ModelShows { get; set; }
        public DbSet<Completed> Completed { get; set; }
        public DbSet<ModelsAftermarket> ModelsAftermarkets { get; set; }
        public DbSet<Manifacturer> Manifacturers { get; set; }
        public DbSet<PurchasedAftermarket> PurchasedAftermarkets { get; set; }
        public DbSet<WishList> WishListModels { get; set; }
        public DbSet<ModelShowCategory> ModelShowCategories { get; set; }
        public DbSet<Varnish> Varnishes { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<PaintAndConsumable> PaintsAndConsumables { get; set; }
        public DbSet<CompletedAftermarket> CompletedAftermarkets { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModelsAftermarket>()
                .HasKey(ma => new { ma.AftermarketId, ma.ModelId });

            modelBuilder.Entity<CompletedAftermarket>()
                .HasKey(ca => new { ca.UsedAftermarketId, ca.CompletedModelId });

            modelBuilder.Entity<CompletedModelShow>()
                .HasKey(msc => new { msc.CompletedId, msc.ModelShowId });

            modelBuilder.Entity<CompletedAftermarket>()
                .HasOne(a => a.Aftermarket)
                .WithMany(ua => ua.CompletedModels)
                .HasForeignKey(fk => fk.UsedAftermarketId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CompletedAftermarket>()
                .HasOne(m => m.Model)
                .WithMany(a => a.UsedAftermarket)
                .HasForeignKey(fk => fk.CompletedModelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ModelsAftermarket>()
                .HasOne(a => a.Aftermarket)
                .WithMany(m => m.AvailableModels)
                .HasForeignKey(fk => fk.AftermarketId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ModelsAftermarket>()
                .HasOne(m => m.Model)
                .WithMany(a => a.PurchasedAftermarket)
                .HasForeignKey(fk => fk.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<ModelShowCategory>()
                .HasIndex(n => n.CategoryName)
                .IsUnique();
        }
    }
}
