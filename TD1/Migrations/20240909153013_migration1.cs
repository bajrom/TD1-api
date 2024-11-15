using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TD1.Migrations
{
    /// <inheritdoc />
    public partial class migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "marque",
                columns: table => new
                {
                    idmarque = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nommarque = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_marque", x => x.idmarque);
                });

            migrationBuilder.CreateTable(
                name: "typeproduit",
                columns: table => new
                {
                    idtypeproduit = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nomtypeproduit = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_typeproduit", x => x.idtypeproduit);
                });

            migrationBuilder.CreateTable(
                name: "produit",
                columns: table => new
                {
                    Idmarque = table.Column<int>(type: "integer", nullable: false),
                    Idtypeproduit = table.Column<int>(type: "integer", nullable: false),
                    idproduit = table.Column<int>(type: "integer", nullable: false),
                    nomProduit = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    nomphoto = table.Column<string>(type: "text", nullable: true),
                    uriphoto = table.Column<string>(type: "text", nullable: true),
                    stockreel = table.Column<int>(type: "integer", nullable: false),
                    stockmin = table.Column<int>(type: "integer", nullable: false),
                    stockmax = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_produits", x => new { x.Idmarque, x.Idtypeproduit });
                    table.ForeignKey(
                        name: "fk_produit_marque",
                        column: x => x.Idmarque,
                        principalTable: "marque",
                        principalColumn: "idmarque");
                    table.ForeignKey(
                        name: "fk_produit_typeproduit",
                        column: x => x.Idtypeproduit,
                        principalTable: "typeproduit",
                        principalColumn: "idtypeproduit");
                });

            migrationBuilder.CreateIndex(
                name: "IX_produit_Idtypeproduit",
                table: "produit",
                column: "Idtypeproduit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "produit");

            migrationBuilder.DropTable(
                name: "marque");

            migrationBuilder.DropTable(
                name: "typeproduit");
        }
    }
}
