using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStockMovementAdjustmentQuantities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NewQuantity",
                table: "StockMovements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreviousQuantity",
                table: "StockMovements",
                type: "int",
                nullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_StockMovements_NewQuantity",
                table: "StockMovements",
                sql: "[NewQuantity] IS NULL OR [NewQuantity] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_StockMovements_PreviousQuantity",
                table: "StockMovements",
                sql: "[PreviousQuantity] IS NULL OR [PreviousQuantity] >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_StockMovements_NewQuantity",
                table: "StockMovements");

            migrationBuilder.DropCheckConstraint(
                name: "CK_StockMovements_PreviousQuantity",
                table: "StockMovements");

            migrationBuilder.DropColumn(
                name: "NewQuantity",
                table: "StockMovements");

            migrationBuilder.DropColumn(
                name: "PreviousQuantity",
                table: "StockMovements");
        }
    }
}
