using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TD1.Models.DataManager;

namespace TD1.Models.EntityFramework
{
    public partial class ProduitDbContext : DbContext
    {
        public ProduitDbContext() { }
        public ProduitDbContext(DbContextOptions<ProduitDbContext> options)
            : base(options) { }

        public virtual DbSet<Marque> Marques { get; set; } = null!;
        public virtual DbSet<Produit> Produits { get; set; } = null!;
        public virtual DbSet<TypeProduit> TypeProduits { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=TD1; uid=postgres; password=postgres;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produit>(entity =>
            {
                entity.HasKey(e => e.Idproduit)
                .HasName("pk_produits");

                entity.HasOne(d => d.IdmarqueNavigation)
                .WithMany(p => p.Produits)
                .HasForeignKey(d => d.Idmarque)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_produit_marque");

                entity.HasOne(d => d.IdtypeproduitNavigation)
                .WithMany(p => p.Produits)
                .HasForeignKey(d => d.Idtypeproduit)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_produit_typeproduit");
            });

            modelBuilder.Entity<TypeProduit>(entity => {
                entity.HasKey(e => e.Idtypeproduit).HasName("pk_typeproduit");
            });

            modelBuilder.Entity<Marque>(entity => {
                entity.HasKey(e => e.Idmarque).HasName("pk_marque");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
