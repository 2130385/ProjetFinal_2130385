using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjetFinal_2130385.Models;

namespace ProjetFinal_2130385.Data
{
    public partial class DronesDatabaseContext : DbContext
    {
        public DronesDatabaseContext()
        {
        }

        public DronesDatabaseContext(DbContextOptions<DronesDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adresse> Adresses { get; set; } = null!;
        public virtual DbSet<Changelog> Changelogs { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Commande> Commandes { get; set; } = null!;
        public virtual DbSet<Courriel> Courriels { get; set; } = null!;
        public virtual DbSet<Drone> Drones { get; set; } = null!;
        public virtual DbSet<Magasin> Magasins { get; set; } = null!;
        public virtual DbSet<Modele> Modeles { get; set; } = null!;
        public virtual DbSet<VwCommande> VwCommandes { get; set; } = null!;
        public virtual DbSet<VwDrone> VwDrones { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DronesDatabase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adresse>(entity =>
            {
                entity.Property(e => e.AdresseId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Changelog>(entity =>
            {
                entity.Property(e => e.InstalledOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.ClientId).ValueGeneratedNever();

                entity.HasOne(d => d.Adresse)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.AdresseId)
                    .HasConstraintName("FK__Client__AdresseI__267ABA7A");
            });

            modelBuilder.Entity<Commande>(entity =>
            {
                entity.Property(e => e.CommandeId).ValueGeneratedNever();

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Commandes)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__Commande__Client__33D4B598");

                entity.HasOne(d => d.Drone)
                    .WithMany(p => p.Commandes)
                    .HasForeignKey(d => d.DroneId)
                    .HasConstraintName("FK__Commande__DroneI__34C8D9D1");

                entity.HasOne(d => d.Magasin)
                    .WithMany(p => p.Commandes)
                    .HasForeignKey(d => d.MagasinId)
                    .HasConstraintName("FK__Commande__Magasi__35BCFE0A");
            });

            modelBuilder.Entity<Courriel>(entity =>
            {
                entity.Property(e => e.CourrielId).ValueGeneratedNever();

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Courriels)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__Courriel__Client__29572725");
            });

            modelBuilder.Entity<Drone>(entity =>
            {
                entity.Property(e => e.DroneId).ValueGeneratedNever();

                entity.HasOne(d => d.Modele)
                    .WithMany(p => p.Drones)
                    .HasForeignKey(d => d.ModeleId)
                    .HasConstraintName("FK__Drone__ModeleID__30F848ED");
            });

            modelBuilder.Entity<Magasin>(entity =>
            {
                entity.Property(e => e.MagasinId).ValueGeneratedNever();

                entity.HasOne(d => d.Adresse)
                    .WithMany(p => p.Magasins)
                    .HasForeignKey(d => d.AdresseId)
                    .HasConstraintName("FK__Magasin__Adresse__2E1BDC42");
            });

            modelBuilder.Entity<Modele>(entity =>
            {
                entity.Property(e => e.ModeleId).ValueGeneratedNever();
            });

            modelBuilder.Entity<VwCommande>(entity =>
            {
                entity.ToView("vw_Commandes", "Magasins");
            });

            modelBuilder.Entity<VwDrone>(entity =>
            {
                entity.ToView("vw_Drones", "Magasins");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}