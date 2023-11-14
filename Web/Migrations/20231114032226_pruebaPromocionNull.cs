using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class pruebaPromocionNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_Promociones_PromocionRefId",
                table: "Delivery");

            migrationBuilder.AlterColumn<int>(
                name: "PromocionRefId",
                table: "Delivery",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_Promociones_PromocionRefId",
                table: "Delivery",
                column: "PromocionRefId",
                principalTable: "Promociones",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_Promociones_PromocionRefId",
                table: "Delivery");

            migrationBuilder.AlterColumn<int>(
                name: "PromocionRefId",
                table: "Delivery",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_Promociones_PromocionRefId",
                table: "Delivery",
                column: "PromocionRefId",
                principalTable: "Promociones",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
