using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class createTakeAwayModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TakeAwayId",
                table: "ProductosPedido",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TakeAway",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MetodoPagoRefId = table.Column<int>(type: "int", nullable: false),
                    HorarioEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PromocionRefId = table.Column<int>(type: "int", nullable: true),
                    PrecioPedido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TakeAway", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TakeAway_MetodoPago_MetodoPagoRefId",
                        column: x => x.MetodoPagoRefId,
                        principalTable: "MetodoPago",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TakeAway_Promociones_PromocionRefId",
                        column: x => x.PromocionRefId,
                        principalTable: "Promociones",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductosPedido_TakeAwayId",
                table: "ProductosPedido",
                column: "TakeAwayId");

            migrationBuilder.CreateIndex(
                name: "IX_TakeAway_MetodoPagoRefId",
                table: "TakeAway",
                column: "MetodoPagoRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TakeAway_PromocionRefId",
                table: "TakeAway",
                column: "PromocionRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductosPedido_TakeAway_TakeAwayId",
                table: "ProductosPedido",
                column: "TakeAwayId",
                principalTable: "TakeAway",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductosPedido_TakeAway_TakeAwayId",
                table: "ProductosPedido");

            migrationBuilder.DropTable(
                name: "TakeAway");

            migrationBuilder.DropIndex(
                name: "IX_ProductosPedido_TakeAwayId",
                table: "ProductosPedido");

            migrationBuilder.DropColumn(
                name: "TakeAwayId",
                table: "ProductosPedido");
        }
    }
}
