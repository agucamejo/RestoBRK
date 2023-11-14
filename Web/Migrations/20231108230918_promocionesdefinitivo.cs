using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class promocionesdefinitivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListaPreciosRefId",
                table: "Promociones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Promociones_ListaPreciosRefId",
                table: "Promociones",
                column: "ListaPreciosRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Promociones_ListaPrecios_ListaPreciosRefId",
                table: "Promociones",
                column: "ListaPreciosRefId",
                principalTable: "ListaPrecios",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promociones_ListaPrecios_ListaPreciosRefId",
                table: "Promociones");

            migrationBuilder.DropIndex(
                name: "IX_Promociones_ListaPreciosRefId",
                table: "Promociones");

            migrationBuilder.DropColumn(
                name: "ListaPreciosRefId",
                table: "Promociones");
        }
    }
}
