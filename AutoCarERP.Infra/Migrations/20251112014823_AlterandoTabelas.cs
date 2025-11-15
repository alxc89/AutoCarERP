using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AutoCarERP.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Veiculo",
                table: "Veiculo");

            migrationBuilder.AddColumn<int>(
                name: "Codigo",
                table: "Veiculo",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_At",
                table: "Veiculo",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted_At",
                table: "Veiculo",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_At",
                table: "Veiculo",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_At",
                table: "ProdutoServico",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted_At",
                table: "ProdutoServico",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_At",
                table: "ProdutoServico",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_At",
                table: "OrdemDeServico",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted_At",
                table: "OrdemDeServico",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_At",
                table: "OrdemDeServico",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_At",
                table: "Clientes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted_At",
                table: "Clientes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_At",
                table: "Clientes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Veiculo",
                table: "Veiculo",
                column: "Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_Placa",
                table: "Veiculo",
                column: "Placa",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Veiculo",
                table: "Veiculo");

            migrationBuilder.DropIndex(
                name: "IX_Veiculo_Placa",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Created_At",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Deleted_At",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Updated_At",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Created_At",
                table: "ProdutoServico");

            migrationBuilder.DropColumn(
                name: "Deleted_At",
                table: "ProdutoServico");

            migrationBuilder.DropColumn(
                name: "Updated_At",
                table: "ProdutoServico");

            migrationBuilder.DropColumn(
                name: "Created_At",
                table: "OrdemDeServico");

            migrationBuilder.DropColumn(
                name: "Deleted_At",
                table: "OrdemDeServico");

            migrationBuilder.DropColumn(
                name: "Updated_At",
                table: "OrdemDeServico");

            migrationBuilder.DropColumn(
                name: "Created_At",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Deleted_At",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Updated_At",
                table: "Clientes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Veiculo",
                table: "Veiculo",
                column: "Placa");
        }
    }
}
