using Microsoft.EntityFrameworkCore;
using Models;

namespace ApiCikanda;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        if (!Directory.Exists("Config"))
            Directory.CreateDirectory("Config");

        if (!Directory.Exists("Config\\Data"))
            Directory.CreateDirectory("Config\\Data");
    }

    public virtual DbSet<Article> Articles { get; set; }
    public virtual DbSet<Achat> Achats { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Command> Commands { get; set; }
    public virtual DbSet<Depense> Depenses { get; set; }
    public virtual DbSet<PointVente> PointVentes { get; set; }
    public virtual DbSet<PrixVente> PrixVentes { get; set; }
    public virtual DbSet<ProduitAchat> ProduitAchats { get; set; }
    public virtual DbSet<ProduitVendu> ProduitVendus { get; set; }
    public virtual DbSet<Taux> Taux { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>()
        .HasOne(a => a.Category)
        .WithMany(a => a.Articles)
        .HasForeignKey(a => a.IdCategory)
        .OnDelete(DeleteBehavior.NoAction)
        .IsRequired();
        modelBuilder.Entity<ProduitAchat>()
        .HasOne(a => a.Article)
        .WithMany(a => a.ProduitAchats)
        .HasForeignKey(a => a.IdAchat)
        .OnDelete(DeleteBehavior.NoAction)
        .IsRequired();
        modelBuilder.Entity<ProduitAchat>()
        .HasOne(a => a.Achat)
        .WithMany(a => a.Produits)
        .HasForeignKey(a => a.IdAchat)
        .OnDelete(DeleteBehavior.NoAction)
        .IsRequired();
        modelBuilder.Entity<ProduitVendu>()
        .HasOne(a => a.Command)
        .WithMany(a => a.ProduitVendus)
        .HasForeignKey(a => a.IdCommand)
        .OnDelete(DeleteBehavior.NoAction)
        .IsRequired();
        modelBuilder.Entity<ProduitVendu>()
        .HasOne(a => a.PrixVente)
        .WithMany(a => a.ProduitVendus)
        .HasForeignKey(a => a.IdPrixVente)
        .OnDelete(DeleteBehavior.NoAction)
        .IsRequired();
        modelBuilder.Entity<PrixVente>()
        .HasOne(a => a.PointVente)
        .WithMany(a => a.PrixVentes)
        .HasForeignKey(a => a.IdPointVente)
        .OnDelete(DeleteBehavior.NoAction)
        .IsRequired();
        modelBuilder.Entity<Command>()
        .HasOne(a => a.PointVente)
        .WithMany(a => a.Commands)
        .HasForeignKey(a => a.IdPointVente)
        .OnDelete(DeleteBehavior.NoAction)
        .IsRequired();
    }
}