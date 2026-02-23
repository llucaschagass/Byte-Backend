using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Byte_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddSolicitacaoAtendimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SolicitacoesAtendimento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumeroMesa = table.Column<int>(type: "integer", nullable: false),
                    Atendida = table.Column<string>(type: "text", nullable: false),
                    AtendidoPorId = table.Column<int>(type: "integer", nullable: true),
                    AtendidoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InseridoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacoesAtendimento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitacoesAtendimento_Usuarios_AtendidoPorId",
                        column: x => x.AtendidoPorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacoesAtendimento_AtendidoPorId",
                table: "SolicitacoesAtendimento",
                column: "AtendidoPorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitacoesAtendimento");
        }
    }
}
