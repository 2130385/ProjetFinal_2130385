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
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Commande> Commandes { get; set; } = null!;
        public virtual DbSet<Courriel> Courriels { get; set; } = null!;
        public virtual DbSet<Drone> Drones { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
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

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.ClientId).ValueGeneratedNever();

                entity.HasOne(d => d.Adresse)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.AdresseId)
                    .HasConstraintName("FK__Client__AdresseI__38996AB5");
            });

            modelBuilder.Entity<Commande>(entity =>
            {
                entity.Property(e => e.CommandeId).ValueGeneratedNever();

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Commandes)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__Commande__Client__45F365D3");

                entity.HasOne(d => d.Drone)
                    .WithMany(p => p.Commandes)
                    .HasForeignKey(d => d.DroneId)
                    .HasConstraintName("FK__Commande__DroneI__46E78A0C");

                entity.HasOne(d => d.Magasin)
                    .WithMany(p => p.Commandes)
                    .HasForeignKey(d => d.MagasinId)
                    .HasConstraintName("FK__Commande__Magasi__47DBAE45");
            });

            modelBuilder.Entity<Courriel>(entity =>
            {
                entity.Property(e => e.CourrielId).ValueGeneratedNever();

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Courriels)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__Courriel__Client__3B75D760");
            });

            modelBuilder.Entity<Drone>(entity =>
            {
                entity.Property(e => e.DroneId).ValueGeneratedNever();

                entity.HasOne(d => d.Modele)
                    .WithMany(p => p.Drones)
                    .HasForeignKey(d => d.ModeleId)
                    .HasConstraintName("FK__Drone__ModeleID__4316F928");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.Identifiant).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Magasin>(entity =>
            {
                entity.Property(e => e.MagasinId).ValueGeneratedNever();

                entity.HasOne(d => d.Adresse)
                    .WithMany(p => p.Magasins)
                    .HasForeignKey(d => d.AdresseId)
                    .HasConstraintName("FK__Magasin__Adresse__403A8C7D");
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
