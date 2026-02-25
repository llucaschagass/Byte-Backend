using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Byte_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuariosPermissoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuariosPermissoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Garcom = table.Column<bool>(type: "boolean", nullable: false),
                    Cozinha = table.Column<bool>(type: "boolean", nullable: false),
                    Gerencia = table.Column<bool>(type: "boolean", nullable: false),
                    Principal = table.Column<int>(type: "integer", nullable: false),
                    InseridoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosPermissoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuariosPermissoes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPermissoes_UsuarioId",
                table: "UsuariosPermissoes",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuariosPermissoes");
        }
    }
}
