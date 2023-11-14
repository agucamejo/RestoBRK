using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class promocionesv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promociones_ListaPrecios_ListaPrecioRefId",
                table: "Promociones");

            migrationBuilder.DropColumn(
                name: "ValidoPara",
                table: "Promociones");

            migrationBuilder.RenameColumn(
                name: "ListaPrecioRefId",
                table: "Promociones",
                newName: "MontoVariacionRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Promociones_ListaPrecioRefId",
                table: "Promociones",
                newName: "IX_Promociones_MontoVariacionRefId");

            migrationBuilder.AddColumn<int>(
                name: "MetodoPagoRefId",
                table: "Promociones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Promociones_MetodoPagoRefId",
                table: "Promociones",
                column: "MetodoPagoRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Promociones_MetodoPago_MetodoPagoRefId",
                table: "Promociones",
                column: "MetodoPagoRefId",
                principalTable: "MetodoPago",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Promociones_MetodoPago_MontoVariacionRefId",
                table: "Promociones",
                column: "MontoVariacionRefId",
                principalTable: "MetodoPago",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promociones_MetodoPago_MetodoPagoRefId",
                table: "Promociones");

            migrationBuilder.DropForeignKey(
                name: "FK_Promociones_MetodoPago_MontoVariacionRefId",
                table: "Promociones");

            migrationBuilder.DropIndex(
                name: "IX_Promociones_MetodoPagoRefId",
                table: "Promociones");

            migrationBuilder.DropColumn(
                name: "MetodoPagoRefId",
                table: "Promociones");

            migrationBuilder.RenameColumn(
                name: "MontoVariacionRefId",
                table: "Promociones",
                newName: "ListaPrecioRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Promociones_MontoVariacionRefId",
                table: "Promociones",
                newName: "IX_Promociones_ListaPrecioRefId");

            migrationBuilder.AddColumn<string>(
                name: "ValidoPara",
                table: "Promociones",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Promociones_ListaPrecios_ListaPrecioRefId",
                table: "Promociones",
                column: "ListaPrecioRefId",
                principalTable: "ListaPrecios",
                principalColumn: "ID");
        }
    }
}
