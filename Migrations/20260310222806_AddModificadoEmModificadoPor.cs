using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Byte_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddModificadoEmModificadoPor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "UsuariosPermissoes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "UsuariosPermissoes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "Usuarios",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "Usuarios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "SolicitacoesAtendimento",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "SolicitacoesAtendimento",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "ProdutosImagens",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "ProdutosImagens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "Produtos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "Produtos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "Pessoas",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "Pessoas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "OpcoesProdutos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "OpcoesProdutos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "ItensComanda",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "ItensComanda",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "Funcionarios",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "Funcionarios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "FilaCozinha",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "FilaCozinha",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "Comandas",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "Comandas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "Clientes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "Clientes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "Categorias",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "Categorias",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "CartoesComanda",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "CartoesComanda",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "Cargos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "Cargos",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "UsuariosPermissoes");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "UsuariosPermissoes");

            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "SolicitacoesAtendimento");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "SolicitacoesAtendimento");

            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "ProdutosImagens");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "ProdutosImagens");

            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "OpcoesProdutos");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "OpcoesProdutos");

            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "ItensComanda");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "ItensComanda");

            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "FilaCozinha");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "FilaCozinha");

            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "Comandas");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "Comandas");

            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "CartoesComanda");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "CartoesComanda");

            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "Cargos");
        }
    }
}
