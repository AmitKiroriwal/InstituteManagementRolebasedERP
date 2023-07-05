using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstituteManagement_Models.Migrations
{
    /// <inheritdoc />
    public partial class addPlanIdtoOrderData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "planId",
                table: "OrderData",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "planId",
                table: "OrderData");
        }
    }
}
