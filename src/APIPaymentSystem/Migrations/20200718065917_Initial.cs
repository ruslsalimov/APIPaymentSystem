using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIPaymentSystem.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentInfo",
                columns: table => new
                {
                    SessionId = table.Column<string>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ArrivalTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInfo", x => x.SessionId);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    SessionId = table.Column<string>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ArrivalTime = table.Column<DateTime>(nullable: false),
                    CardNumber = table.Column<string>(maxLength: 19, nullable: false),
                    TimePayment = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.SessionId);
                });

            migrationBuilder.CreateTable(
                name: "CardInfo",
                columns: table => new
                {
                    CardNumber = table.Column<string>(maxLength: 19, nullable: false),
                    VerificationNumber = table.Column<int>(nullable: false),
                    CardDate = table.Column<DateTime>(nullable: false),
                    PaymentInfoSessionId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardInfo", x => x.CardNumber);
                    table.ForeignKey(
                        name: "FK_CardInfo_PaymentInfo_PaymentInfoSessionId",
                        column: x => x.PaymentInfoSessionId,
                        principalTable: "PaymentInfo",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardInfo_PaymentInfoSessionId",
                table: "CardInfo",
                column: "PaymentInfoSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardInfo");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "PaymentInfo");
        }
    }
}
