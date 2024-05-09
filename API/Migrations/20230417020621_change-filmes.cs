using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class changefilmes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_filmes_generos_GeneroId",
                table: "filmes");

            migrationBuilder.DropIndex(
                name: "IX_filmes_GeneroId",
                table: "filmes");

            migrationBuilder.DropColumn(
                name: "GeneroId",
                table: "filmes");

            migrationBuilder.AddColumn<int>(
                name: "GeneroIdGenero",
                table: "filmes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdGenero",
                table: "filmes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_filmes_GeneroIdGenero",
                table: "filmes",
                column: "GeneroIdGenero");

            migrationBuilder.AddForeignKey(
                name: "FK_filmes_generos_GeneroIdGenero",
                table: "filmes",
                column: "GeneroIdGenero",
                principalTable: "generos",
                principalColumn: "IdGenero");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_filmes_generos_GeneroIdGenero",
                table: "filmes");

            migrationBuilder.DropIndex(
                name: "IX_filmes_GeneroIdGenero",
                table: "filmes");

            migrationBuilder.DropColumn(
                name: "GeneroIdGenero",
                table: "filmes");

            migrationBuilder.DropColumn(
                name: "IdGenero",
                table: "filmes");

            migrationBuilder.AddColumn<int>(
                name: "GeneroId",
                table: "filmes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_filmes_GeneroId",
                table: "filmes",
                column: "GeneroId");

            migrationBuilder.AddForeignKey(
                name: "FK_filmes_generos_GeneroId",
                table: "filmes",
                column: "GeneroId",
                principalTable: "generos",
                principalColumn: "IdGenero",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
