using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class createMesasModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MesasId",
                table: "ProductosPedido",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Mesas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MetodoPagoRefId = table.Column<int>(type: "int", nullable: false),
                    NroMesa = table.Column<int>(type: "int", nullable: false),
                    PromocionRefId = table.Column<int>(type: "int", nullable: true),
                    PrecioPedido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Mesas_MetodoPago_MetodoPagoRefId",
                        column: x => x.MetodoPagoRefId,
                        principalTable: "MetodoPago",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mesas_Promociones_PromocionRefId",
                        column: x => x.PromocionRefId,
                        principalTable: "Promociones",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductosPedido_MesasId",
                table: "ProductosPedido",
                column: "MesasId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesas_MetodoPagoRefId",
                table: "Mesas",
                column: "MetodoPagoRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesas_PromocionRefId",
                table: "Mesas",
                column: "PromocionRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductosPedido_Mesas_MesasId",
                table: "ProductosPedido",
                column: "MesasId",
                principalTable: "Mesas",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductosPedido_Mesas_MesasId",
                table: "ProductosPedido");

            migrationBuilder.DropTable(
                name: "Mesas");

            migrationBuilder.DropIndex(
                name: "IX_ProductosPedido_MesasId",
                table: "ProductosPedido");

            migrationBuilder.DropColumn(
                name: "MesasId",
                table: "ProductosPedido");
        }
    }
}
