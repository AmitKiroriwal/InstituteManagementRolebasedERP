using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstituteManagement_Models.Migrations
{
    /// <inheritdoc />
    public partial class updatePaymentfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Payments",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Razorpay_Signature",
                table: "Payments",
                newName: "razorpay_Signature");

            migrationBuilder.RenameColumn(
                name: "Razorpay_Payment_Id",
                table: "Payments",
                newName: "razorpay_Payment_Id");

            migrationBuilder.RenameColumn(
                name: "Razorpay_Order_Id",
                table: "Payments",
                newName: "razorpay_Order_Id");

            migrationBuilder.RenameColumn(
                name: "Method",
                table: "Payments",
                newName: "method");

            migrationBuilder.RenameColumn(
                name: "Desc",
                table: "Payments",
                newName: "desc");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "Payments",
                newName: "currency");

            migrationBuilder.RenameColumn(
                name: "Capat",
                table: "Payments",
                newName: "capat");

            migrationBuilder.RenameColumn(
                name: "Authat",
                table: "Payments",
                newName: "authat");

            migrationBuilder.RenameColumn(
                name: "AuthCode",
                table: "Payments",
                newName: "authcode");

            migrationBuilder.RenameColumn(
                name: "AmountPaid",
                table: "Payments",
                newName: "amountPaid");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Payments",
                newName: "ctype");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Payments",
                newName: "srNo");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Payments",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "Cardtype",
                table: "Payments",
                newName: "AppUserId");

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Payments",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "razorpay_Signature",
                table: "Payments",
                newName: "Razorpay_Signature");

            migrationBuilder.RenameColumn(
                name: "razorpay_Payment_Id",
                table: "Payments",
                newName: "Razorpay_Payment_Id");

            migrationBuilder.RenameColumn(
                name: "razorpay_Order_Id",
                table: "Payments",
                newName: "Razorpay_Order_Id");

            migrationBuilder.RenameColumn(
                name: "method",
                table: "Payments",
                newName: "Method");

            migrationBuilder.RenameColumn(
                name: "desc",
                table: "Payments",
                newName: "Desc");

            migrationBuilder.RenameColumn(
                name: "currency",
                table: "Payments",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "capat",
                table: "Payments",
                newName: "Capat");

            migrationBuilder.RenameColumn(
                name: "authcode",
                table: "Payments",
                newName: "AuthCode");

            migrationBuilder.RenameColumn(
                name: "authat",
                table: "Payments",
                newName: "Authat");

            migrationBuilder.RenameColumn(
                name: "amountPaid",
                table: "Payments",
                newName: "AmountPaid");

            migrationBuilder.RenameColumn(
                name: "srNo",
                table: "Payments",
                newName: "SessionId");

            migrationBuilder.RenameColumn(
                name: "ctype",
                table: "Payments",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "Payments",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Payments",
                newName: "Cardtype");
        }
    }
}
