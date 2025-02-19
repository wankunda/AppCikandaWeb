using Microsoft.EntityFrameworkCore;
using ModelsServices.Models;
using ModelsServices.Utilities;

namespace ModelsServices.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Command> Commands { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<ProduitsAppro> ProduitsAppros { get; set; }
        public DbSet<ProduitsVendus> ProduitsVendus { get; set; }
        public DbSet<PrixVente> PrixVentes { get; set; }
        public DbSet<PointVente> PointVentes { get; set; }
        public DbSet<Depense> Depenses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Appro> Appros { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Taux> Taux { get; set; }

        string data = @"Config\Data";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(@$"Data Source={data}\GestVente.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produit>()
                .HasOne(e => e.Category)
                .WithMany(e => e.Produits)
                .HasForeignKey(e => e.IdCategory)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            modelBuilder.Entity<ProduitsAppro>()
                .HasOne(e => e.Produit)
                .WithMany(e => e.Appros)
                .HasForeignKey(e => e.IdProduit)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            modelBuilder.Entity<ProduitsAppro>()
                .HasOne(e => e.Appro)
                .WithMany(e => e.Produits)
                .HasForeignKey(e => e.IdAppro)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            modelBuilder.Entity<ProduitsVendus>()
                .HasOne(e => e.Command)
                .WithMany(e => e.Produits)
                .HasForeignKey(e => e.IdCommand)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            modelBuilder.Entity<ProduitsVendus>()
                .HasOne(e => e.PrixVente)
                .WithMany(e => e.Produits)
                .HasForeignKey(e => e.IdPrixVente)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            modelBuilder.Entity<PrixVente>()
                .HasOne(e => e.Produit)
                .WithMany(e => e.PrixVentes)
                .HasForeignKey(e => e.IdProduit)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            modelBuilder.Entity<PrixVente>()
                .HasOne(e => e.PointVente)
                .WithMany(e => e.PrixVentes)
                .HasForeignKey(e => e.IdPointVente)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            modelBuilder.Entity<Command>()
                .HasOne(e => e.PointVente)
                .WithMany(e => e.Commands)
                .HasForeignKey(e => e.IdPointVente)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Username = "admin",
                    Role = (int)UserRole.Admin,
                    Password = new Password("1234").ToString(),
                    Nom = "ADMINISTRATEUR",
                    Prenom = "Administrateur",
                    Id = Guid.NewGuid(),
                    IdPointVente = Guid.Empty,
                });
        }
    }
}
