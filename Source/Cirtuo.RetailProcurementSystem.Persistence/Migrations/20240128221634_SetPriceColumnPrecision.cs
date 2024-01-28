using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cirtuo.RetailProcurementSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SetPriceColumnPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "item_price",
                table: "supplier_store_item",
                type: "numeric(1000,5)",
                precision: 1000,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "item_price",
                table: "order_item",
                type: "numeric(1000,5)",
                precision: 1000,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_price",
                table: "order",
                type: "numeric(1000,5)",
                precision: 1000,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "item_price",
                table: "supplier_store_item",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(1000,5)",
                oldPrecision: 1000,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "item_price",
                table: "order_item",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(1000,5)",
                oldPrecision: 1000,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "total_price",
                table: "order",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(1000,5)",
                oldPrecision: 1000,
                oldScale: 5);
        }
    }
}
