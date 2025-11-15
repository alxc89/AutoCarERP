using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AutoCarERP.Infra.Migrations
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
                    Codigo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Telefone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    CpfCnpj = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Endereco = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "OrdemDeServico",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HoraAbertura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HoraFechamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Veiculo = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Cliente = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ProdutoServico = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "numeric(7,2)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "numeric(7,2)", nullable: false),
                    Observacao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdemDeServico", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoServico",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Fornecedor = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Custo = table.Column<decimal>(type: "numeric(7,2)", nullable: true),
                    Valor = table.Column<decimal>(type: "numeric(7,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoServico", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    Placa = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Marca = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Modelo = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Cor = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Ano = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.Placa);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "OrdemDeServico");

            migrationBuilder.DropTable(
                name: "ProdutoServico");

            migrationBuilder.DropTable(
                name: "Veiculo");
        }
    }
}
