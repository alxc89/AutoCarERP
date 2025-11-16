using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoCarERP.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InclusaoRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cliente",
                table: "OrdemDeServico");

            migrationBuilder.DropColumn(
                name: "ProdutoServico",
                table: "OrdemDeServico");

            migrationBuilder.DropColumn(
                name: "Veiculo",
                table: "OrdemDeServico");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "OrdemDeServico",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProdutoServicoId",
                table: "OrdemDeServico",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VeiculoId",
                table: "OrdemDeServico",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrdemDeServico_ClienteId",
                table: "OrdemDeServico",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdemDeServico_ProdutoServicoId",
                table: "OrdemDeServico",
                column: "ProdutoServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdemDeServico_VeiculoId",
                table: "OrdemDeServico",
                column: "VeiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdemDeServico_Clientes_ClienteId",
                table: "OrdemDeServico",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdemDeServico_ProdutoServico_ProdutoServicoId",
                table: "OrdemDeServico",
                column: "ProdutoServicoId",
                principalTable: "ProdutoServico",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdemDeServico_Veiculo_VeiculoId",
                table: "OrdemDeServico",
                column: "VeiculoId",
                principalTable: "Veiculo",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdemDeServico_Clientes_ClienteId",
                table: "OrdemDeServico");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdemDeServico_ProdutoServico_ProdutoServicoId",
                table: "OrdemDeServico");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdemDeServico_Veiculo_VeiculoId",
                table: "OrdemDeServico");

            migrationBuilder.DropIndex(
                name: "IX_OrdemDeServico_ClienteId",
                table: "OrdemDeServico");

            migrationBuilder.DropIndex(
                name: "IX_OrdemDeServico_ProdutoServicoId",
                table: "OrdemDeServico");

            migrationBuilder.DropIndex(
                name: "IX_OrdemDeServico_VeiculoId",
                table: "OrdemDeServico");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "OrdemDeServico");

            migrationBuilder.DropColumn(
                name: "ProdutoServicoId",
                table: "OrdemDeServico");

            migrationBuilder.DropColumn(
                name: "VeiculoId",
                table: "OrdemDeServico");

            migrationBuilder.AddColumn<string>(
                name: "Cliente",
                table: "OrdemDeServico",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProdutoServico",
                table: "OrdemDeServico",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Veiculo",
                table: "OrdemDeServico",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }
    }
}
