using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    
    public partial class CreateSessaoAndOperadorSessao : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Criar a tabela relacional operadores_sessoes
            migrationBuilder.CreateTable(
                name: "operadores_sessoes",
                columns: table => new
                {
                    IdOperadorSessao = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF = table.Column<string>(nullable: false),  // Usando CPF como chave estrangeira
                    IdSessao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operadores_sessoes", x => x.IdOperadorSessao);
                    table.ForeignKey(
                        name: "FK_operadores_sessoes_operadores_CPF",
                        column: x => x.CPF,
                        principalTable: "operadores",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_operadores_sessoes_sessoes_IdSessao",
                        column: x => x.IdSessao,
                        principalTable: "sessoes",
                        principalColumn: "IdSessao",
                        onDelete: ReferentialAction.Cascade);
                });

            // Adicionar índices para as chaves estrangeiras
            migrationBuilder.CreateIndex(
                name: "IX_operadores_sessoes_CPF",
                table: "operadores_sessoes",
                column: "CPF");

            migrationBuilder.CreateIndex(
                name: "IX_operadores_sessoes_IdSessao",
                table: "operadores_sessoes",
                column: "IdSessao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remover a tabela relacional
            migrationBuilder.DropTable(
                name: "operadores_sessoes");
        }
    }
}
