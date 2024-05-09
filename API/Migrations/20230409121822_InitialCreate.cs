using System;
using System.Collections.Generic;
using API.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CPF = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    dataNascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "compras",
                columns: table => new
                {
                    idCompra = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_compras", x => x.idCompra);
                });

            migrationBuilder.CreateTable(
                name: "generos",
                columns: table => new
                {
                    IdGenero = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tipo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_generos", x => x.IdGenero);
                });

            migrationBuilder.CreateTable(
                name: "operadores",
                columns: table => new
                {
                    CPF = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operadores", x => x.CPF);
                });

            migrationBuilder.CreateTable(
                name: "salas",
                columns: table => new
                {
                    Numero = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QtdAssentos = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salas", x => x.Numero);
                });

            migrationBuilder.CreateTable(
                name: "pagamentos",
                columns: table => new
                {
                    IdPagamento = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    Pago = table.Column<bool>(type: "boolean", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClienteIdCliente = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pagamentos", x => x.IdPagamento);
                    table.ForeignKey(
                        name: "FK_pagamentos_Clientes_ClienteIdCliente",
                        column: x => x.ClienteIdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "filmes",
                columns: table => new
                {
                    IdFilme = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Poster = table.Column<string>(type: "text", nullable: false),
                    Duracao = table.Column<int>(type: "integer", nullable: false),
                    GeneroIdGenero = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filmes", x => x.IdFilme);
                    table.ForeignKey(
                        name: "FK_filmes_generos_GeneroIdGenero",
                        column: x => x.GeneroIdGenero,
                        principalTable: "generos",
                        principalColumn: "IdGenero",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assentos",
                columns: table => new
                {
                    Identificador = table.Column<string>(type: "text", nullable: false),
                    Ocupado = table.Column<bool>(type: "boolean", nullable: false),
                    SalaNumero = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assentos", x => x.Identificador);
                    table.ForeignKey(
                        name: "FK_assentos_salas_SalaNumero",
                        column: x => x.SalaNumero,
                        principalTable: "salas",
                        principalColumn: "Numero",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sessoes",
                columns: table => new
                {
                    IdSessao = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HoraInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HoraFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    FilmeIdFilme = table.Column<int>(type: "integer", nullable: false),
                    SalaNumero = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sessoes", x => x.IdSessao);
                    table.ForeignKey(
                        name: "FK_sessoes_filmes_FilmeIdFilme",
                        column: x => x.FilmeIdFilme,
                        principalTable: "filmes",
                        principalColumn: "IdFilme",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sessoes_salas_SalaNumero",
                        column: x => x.SalaNumero,
                        principalTable: "salas",
                        principalColumn: "Numero",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ingressos",
                columns: table => new
                {
                    CodIngresso = table.Column<string>(type: "text", nullable: false),
                    AssentoIdentificador = table.Column<string>(type: "text", nullable: true),
                    SessaoIdSessao = table.Column<int>(type: "integer", nullable: false),
                    CompraidCompra = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingressos", x => x.CodIngresso);
                    table.ForeignKey(
                        name: "FK_ingressos_assentos_AssentoIdentificador",
                        column: x => x.AssentoIdentificador,
                        principalTable: "assentos",
                        principalColumn: "Identificador");
                    table.ForeignKey(
                        name: "FK_ingressos_compras_CompraidCompra",
                        column: x => x.CompraidCompra,
                        principalTable: "compras",
                        principalColumn: "idCompra");
                    table.ForeignKey(
                        name: "FK_ingressos_sessoes_SessaoIdSessao",
                        column: x => x.SessaoIdSessao,
                        principalTable: "sessoes",
                        principalColumn: "IdSessao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_assentos_SalaNumero",
                table: "assentos",
                column: "SalaNumero");

            migrationBuilder.CreateIndex(
                name: "IX_filmes_GeneroIdGenero",
                table: "filmes",
                column: "GeneroIdGenero");

            migrationBuilder.CreateIndex(
                name: "IX_ingressos_AssentoIdentificador",
                table: "ingressos",
                column: "AssentoIdentificador");

            migrationBuilder.CreateIndex(
                name: "IX_ingressos_CompraidCompra",
                table: "ingressos",
                column: "CompraidCompra");

            migrationBuilder.CreateIndex(
                name: "IX_ingressos_SessaoIdSessao",
                table: "ingressos",
                column: "SessaoIdSessao");

            migrationBuilder.CreateIndex(
                name: "IX_pagamentos_ClienteIdCliente",
                table: "pagamentos",
                column: "ClienteIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_sessoes_FilmeIdFilme",
                table: "sessoes",
                column: "FilmeIdFilme");

            migrationBuilder.CreateIndex(
                name: "IX_sessoes_SalaNumero",
                table: "sessoes",
            column: "SalaNumero");


        }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ingressos");

            migrationBuilder.DropTable(
                name: "operadores");

            migrationBuilder.DropTable(
                name: "pagamentos");

            migrationBuilder.DropTable(
                name: "assentos");

            migrationBuilder.DropTable(
                name: "compras");

            migrationBuilder.DropTable(
                name: "sessoes");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "filmes");

            migrationBuilder.DropTable(
                name: "salas");

            migrationBuilder.DropTable(
                name: "generos");
        }
    }
}
