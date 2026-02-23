using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Byte_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarSistemaComandas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartoesComanda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumeroCartao = table.Column<int>(type: "integer", nullable: false),
                    CodigoRfid = table.Column<string>(type: "text", nullable: false),
                    InseridoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartoesComanda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comandas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CartaoId = table.Column<int>(type: "integer", nullable: false),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    AbertaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechadaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InseridoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comandas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comandas_CartoesComanda_CartaoId",
                        column: x => x.CartaoId,
                        principalTable: "CartoesComanda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comandas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItensComanda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ComandaId = table.Column<int>(type: "integer", nullable: false),
                    ProdutoId = table.Column<int>(type: "integer", nullable: false),
                    OpcaoProdutoId = table.Column<int>(type: "integer", nullable: true),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    InseridoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensComanda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensComanda_Comandas_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensComanda_OpcoesProdutos_OpcaoProdutoId",
                        column: x => x.OpcaoProdutoId,
                        principalTable: "OpcoesProdutos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItensComanda_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FilaCozinha",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemComandaId = table.Column<int>(type: "integer", nullable: false),
                    StatusPreparo = table.Column<string>(type: "text", nullable: false),
                    UltimaAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    InseridoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilaCozinha", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilaCozinha_ItensComanda_ItemComandaId",
                        column: x => x.ItemComandaId,
                        principalTable: "ItensComanda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartoesComanda_CodigoRfid",
                table: "CartoesComanda",
                column: "CodigoRfid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartoesComanda_NumeroCartao",
                table: "CartoesComanda",
                column: "NumeroCartao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comandas_CartaoId",
                table: "Comandas",
                column: "CartaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comandas_ClienteId",
                table: "Comandas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_FilaCozinha_ItemComandaId",
                table: "FilaCozinha",
                column: "ItemComandaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItensComanda_ComandaId",
                table: "ItensComanda",
                column: "ComandaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensComanda_OpcaoProdutoId",
                table: "ItensComanda",
                column: "OpcaoProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensComanda_ProdutoId",
                table: "ItensComanda",
                column: "ProdutoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilaCozinha");

            migrationBuilder.DropTable(
                name: "ItensComanda");

            migrationBuilder.DropTable(
                name: "Comandas");

            migrationBuilder.DropTable(
                name: "CartoesComanda");
        }
    }
}
