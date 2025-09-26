using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entrega_Act4.Migrations
{
    /// <inheritdoc />
    public partial class updateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DetalleEnvios_IdProducto",
                table: "DetalleEnvios");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleEnvios_IdProducto",
                table: "DetalleEnvios",
                column: "IdProducto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DetalleEnvios_IdProducto",
                table: "DetalleEnvios");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleEnvios_IdProducto",
                table: "DetalleEnvios",
                column: "IdProducto",
                unique: true);
        }
    }
}
