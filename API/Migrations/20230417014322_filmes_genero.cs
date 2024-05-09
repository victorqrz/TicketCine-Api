using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class filmes_genero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assentos_salas_SalaNumero",
                table: "assentos");

            migrationBuilder.DropForeignKey(
                name: "FK_filmes_generos_GeneroIdGenero",
                table: "filmes");

            migrationBuilder.DropForeignKey(
                name: "FK_sessoes_salas_SalaNumero",
                table: "sessoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_salas",
                table: "salas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ingressos",
                table: "ingressos");

            migrationBuilder.RenameColumn(
                name: "SalaNumero",
                table: "sessoes",
                newName: "SalaIdSala");

            migrationBuilder.RenameIndex(
                name: "IX_sessoes_SalaNumero",
                table: "sessoes",
                newName: "IX_sessoes_SalaIdSala");

            migrationBuilder.RenameColumn(
                name: "GeneroIdGenero",
                table: "filmes",
                newName: "GeneroId");

            migrationBuilder.RenameIndex(
                name: "IX_filmes_GeneroIdGenero",
                table: "filmes",
                newName: "IX_filmes_GeneroId");

            migrationBuilder.RenameColumn(
                name: "SalaNumero",
                table: "assentos",
                newName: "SalaIdSala");

            migrationBuilder.RenameIndex(
                name: "IX_assentos_SalaNumero",
                table: "assentos",
                newName: "IX_assentos_SalaIdSala");

            migrationBuilder.AlterColumn<int>(
                name: "Numero",
                table: "salas",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "IdSala",
                table: "salas",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "IdIngresso",
                table: "ingressos",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AddPrimaryKey(
                name: "PK_salas",
                table: "salas",
                column: "IdSala");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ingressos",
                table: "ingressos",
                column: "IdIngresso");

            migrationBuilder.AddForeignKey(
                name: "FK_assentos_salas_SalaIdSala",
                table: "assentos",
                column: "SalaIdSala",
                principalTable: "salas",
                principalColumn: "IdSala",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_filmes_generos_GeneroId",
                table: "filmes",
                column: "GeneroId",
                principalTable: "generos",
                principalColumn: "IdGenero",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sessoes_salas_SalaIdSala",
                table: "sessoes",
                column: "SalaIdSala",
                principalTable: "salas",
                principalColumn: "IdSala",
                onDelete: ReferentialAction.Cascade);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assentos_salas_SalaIdSala",
                table: "assentos");

            migrationBuilder.DropForeignKey(
                name: "FK_filmes_generos_GeneroId",
                table: "filmes");

            migrationBuilder.DropForeignKey(
                name: "FK_sessoes_salas_SalaIdSala",
                table: "sessoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_salas",
                table: "salas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ingressos",
                table: "ingressos");

            migrationBuilder.DropColumn(
                name: "IdSala",
                table: "salas");

            migrationBuilder.DropColumn(
                name: "IdIngresso",
                table: "ingressos");

            migrationBuilder.RenameColumn(
                name: "SalaIdSala",
                table: "sessoes",
                newName: "SalaNumero");

            migrationBuilder.RenameIndex(
                name: "IX_sessoes_SalaIdSala",
                table: "sessoes",
                newName: "IX_sessoes_SalaNumero");

            migrationBuilder.RenameColumn(
                name: "GeneroId",
                table: "filmes",
                newName: "GeneroIdGenero");

            migrationBuilder.RenameIndex(
                name: "IX_filmes_GeneroId",
                table: "filmes",
                newName: "IX_filmes_GeneroIdGenero");

            migrationBuilder.RenameColumn(
                name: "SalaIdSala",
                table: "assentos",
                newName: "SalaNumero");

            migrationBuilder.RenameIndex(
                name: "IX_assentos_SalaIdSala",
                table: "assentos",
                newName: "IX_assentos_SalaNumero");

            migrationBuilder.AlterColumn<int>(
                name: "Numero",
                table: "salas",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_salas",
                table: "salas",
                column: "Numero");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ingressos",
                table: "ingressos",
                column: "CodIngresso");

            migrationBuilder.AddForeignKey(
                name: "FK_assentos_salas_SalaNumero",
                table: "assentos",
                column: "SalaNumero",
                principalTable: "salas",
                principalColumn: "Numero",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_filmes_generos_GeneroIdGenero",
                table: "filmes",
                column: "GeneroIdGenero",
                principalTable: "generos",
                principalColumn: "IdGenero",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sessoes_salas_SalaNumero",
                table: "sessoes",
                column: "SalaNumero",
                principalTable: "salas",
                principalColumn: "Numero",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
