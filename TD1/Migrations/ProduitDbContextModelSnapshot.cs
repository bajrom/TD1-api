﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TD1.Models.EntityFramework;

#nullable disable

namespace TD1.Migrations
{
    [DbContext(typeof(ProduitDbContext))]
    partial class ProduitDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TD1.Models.EntityFramework.Marque", b =>
                {
                    b.Property<int>("Idmarque")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idmarque");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Idmarque"));

                    b.Property<string>("Nommarque")
                        .HasColumnType("text")
                        .HasColumnName("nommarque");

                    b.HasKey("Idmarque")
                        .HasName("pk_marque");

                    b.ToTable("marque");
                });

            modelBuilder.Entity("TD1.Models.EntityFramework.Produit", b =>
                {
                    b.Property<int>("Idproduit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idproduit");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Idproduit"));

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("Idmarque")
                        .HasColumnType("integer");

                    b.Property<int>("Idtypeproduit")
                        .HasColumnType("integer");

                    b.Property<string>("Nomphoto")
                        .HasColumnType("text")
                        .HasColumnName("nomphoto");

                    b.Property<string>("Nomproduit")
                        .HasColumnType("text")
                        .HasColumnName("nomProduit");

                    b.Property<int>("Stockmax")
                        .HasColumnType("integer")
                        .HasColumnName("stockmax");

                    b.Property<int>("Stockmin")
                        .HasColumnType("integer")
                        .HasColumnName("stockmin");

                    b.Property<int>("Stockreel")
                        .HasColumnType("integer")
                        .HasColumnName("stockreel");

                    b.Property<string>("Uriphoto")
                        .HasColumnType("text")
                        .HasColumnName("uriphoto");

                    b.HasKey("Idproduit")
                        .HasName("pk_produits");

                    b.HasIndex("Idmarque");

                    b.HasIndex("Idtypeproduit");

                    b.ToTable("produit");
                });

            modelBuilder.Entity("TD1.Models.EntityFramework.TypeProduit", b =>
                {
                    b.Property<int>("Idtypeproduit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idtypeproduit");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Idtypeproduit"));

                    b.Property<string>("Nomtypeproduit")
                        .HasColumnType("text")
                        .HasColumnName("nomtypeproduit");

                    b.HasKey("Idtypeproduit")
                        .HasName("pk_typeproduit");

                    b.ToTable("typeproduit");
                });

            modelBuilder.Entity("TD1.Models.EntityFramework.Produit", b =>
                {
                    b.HasOne("TD1.Models.EntityFramework.Marque", "IdmarqueNavigation")
                        .WithMany("Produits")
                        .HasForeignKey("Idmarque")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_produit_marque");

                    b.HasOne("TD1.Models.EntityFramework.TypeProduit", "IdtypeproduitNavigation")
                        .WithMany("Produits")
                        .HasForeignKey("Idtypeproduit")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_produit_typeproduit");

                    b.Navigation("IdmarqueNavigation");

                    b.Navigation("IdtypeproduitNavigation");
                });

            modelBuilder.Entity("TD1.Models.EntityFramework.Marque", b =>
                {
                    b.Navigation("Produits");
                });

            modelBuilder.Entity("TD1.Models.EntityFramework.TypeProduit", b =>
                {
                    b.Navigation("Produits");
                });
#pragma warning restore 612, 618
        }
    }
}
