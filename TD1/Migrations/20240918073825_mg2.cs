using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TD1.Migrations
{
    /// <inheritdoc />
    public partial class mg2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_produit_marque",
                table: "produit");

            migrationBuilder.DropForeignKey(
                name: "fk_produit_typeproduit",
                table: "produit");

            migrationBuilder.DropPrimaryKey(
                name: "pk_produits",
                table: "produit");

            migrationBuilder.AlterColumn<int>(
                name: "idproduit",
                table: "produit",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_produits",
                table: "produit",
                column: "idproduit");

            migrationBuilder.CreateIndex(
                name: "IX_produit_Idmarque",
                table: "produit",
                column: "Idmarque");

            migrationBuilder.AddForeignKey(
                name: "fk_produit_marque",
                table: "produit",
                column: "Idmarque",
                principalTable: "marque",
                principalColumn: "idmarque",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_produit_typeproduit",
                table: "produit",
                column: "Idtypeproduit",
                principalTable: "typeproduit",
                principalColumn: "idtypeproduit",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_produit_marque",
                table: "produit");

            migrationBuilder.DropForeignKey(
                name: "fk_produit_typeproduit",
                table: "produit");

            migrationBuilder.DropPrimaryKey(
                name: "pk_produits",
                table: "produit");

            migrationBuilder.DropIndex(
                name: "IX_produit_Idmarque",
                table: "produit");

            migrationBuilder.AlterColumn<int>(
                name: "idproduit",
                table: "produit",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_produits",
                table: "produit",
                columns: new[] { "Idmarque", "Idtypeproduit" });

            migrationBuilder.AddForeignKey(
                name: "fk_produit_marque",
                table: "produit",
                column: "Idmarque",
                principalTable: "marque",
                principalColumn: "idmarque");

            migrationBuilder.AddForeignKey(
                name: "fk_produit_typeproduit",
                table: "produit",
                column: "Idtypeproduit",
                principalTable: "typeproduit",
                principalColumn: "idtypeproduit");
        }
    }
}
