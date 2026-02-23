using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Byte_Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategoriaPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categorias",
                newName: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "Categorias",
                newName: "Id");
        }
    }
}
