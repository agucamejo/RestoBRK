using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class cambiosMomentaneosProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductosPedido_Bebida_BebidaRefId",
                table: "ProductosPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductosPedido_Plato_PlatoRefId",
                table: "ProductosPedido");

            migrationBuilder.DropIndex(
                name: "IX_ProductosPedido_BebidaRefId",
                table: "ProductosPedido");

            migrationBuilder.DropColumn(
                name: "BebidaRefId",
                table: "ProductosPedido");

            migrationBuilder.RenameColumn(
                name: "PlatoRefId",
                table: "ProductosPedido",
                newName: "ListaPrecioRefId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductosPedido_PlatoRefId",
                table: "ProductosPedido",
                newName: "IX_ProductosPedido_ListaPrecioRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductosPedido_ListaPrecios_ListaPrecioRefId",
                table: "ProductosPedido",
                column: "ListaPrecioRefId",
                principalTable: "ListaPrecios",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductosPedido_ListaPrecios_ListaPrecioRefId",
                table: "ProductosPedido");

            migrationBuilder.RenameColumn(
                name: "ListaPrecioRefId",
                table: "ProductosPedido",
                newName: "PlatoRefId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductosPedido_ListaPrecioRefId",
                table: "ProductosPedido",
                newName: "IX_ProductosPedido_PlatoRefId");

            migrationBuilder.AddColumn<int>(
                name: "BebidaRefId",
                table: "ProductosPedido",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductosPedido_BebidaRefId",
                table: "ProductosPedido",
                column: "BebidaRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductosPedido_Bebida_BebidaRefId",
                table: "ProductosPedido",
                column: "BebidaRefId",
                principalTable: "Bebida",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductosPedido_Plato_PlatoRefId",
                table: "ProductosPedido",
                column: "PlatoRefId",
                principalTable: "Plato",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
