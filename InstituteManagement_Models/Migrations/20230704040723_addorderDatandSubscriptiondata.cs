using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstituteManagement_Models.Migrations
{
    /// <inheritdoc />
    public partial class addorderDatandSubscriptiondata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Merchant_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Redirect_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cancel_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Billing_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Billing_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Billing_city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Billing_state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Billing_zip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Billing_country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Billing_tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Billing_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Razorpay_payment_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Razorpay_order_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Razorpay_signature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhatDoYouWantToPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientEMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deposited = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionData_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionData_Plans_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Plans",
                        principalColumn: "PlanID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionData_AppUserId",
                table: "SubscriptionData",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionData_OrderId",
                table: "SubscriptionData",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderData");

            migrationBuilder.DropTable(
                name: "SubscriptionData");
        }
    }
}
