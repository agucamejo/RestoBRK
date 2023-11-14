using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class pruebaDelivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MetodoPagoRefId = table.Column<int>(type: "int", nullable: false),
                    PromocionRefId = table.Column<int>(type: "int", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DireccionCliente = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Delivery_MetodoPago_MetodoPagoRefId",
                        column: x => x.MetodoPagoRefId,
                        principalTable: "MetodoPago",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Delivery_Promociones_PromocionRefId",
                        column: x => x.PromocionRefId,
                        principalTable: "Promociones",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductosPedido",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlatoRefId = table.Column<int>(type: "int", nullable: false),
                    BebidaRefId = table.Column<int>(type: "int", nullable: false),
                    DeliveryId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosPedido", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductosPedido_Bebida_BebidaRefId",
                        column: x => x.BebidaRefId,
                        principalTable: "Bebida",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductosPedido_Delivery_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Delivery",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ProductosPedido_Plato_PlatoRefId",
                        column: x => x.PlatoRefId,
                        principalTable: "Plato",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_MetodoPagoRefId",
                table: "Delivery",
                column: "MetodoPagoRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_PromocionRefId",
                table: "Delivery",
                column: "PromocionRefId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosPedido_BebidaRefId",
                table: "ProductosPedido",
                column: "BebidaRefId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosPedido_DeliveryId",
                table: "ProductosPedido",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosPedido_PlatoRefId",
                table: "ProductosPedido",
                column: "PlatoRefId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductosPedido");

            migrationBuilder.DropTable(
                name: "Delivery");
        }
    }
}
