using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonKey.Data.Migrations
{
    /// <inheritdoc />
    public partial class IntruduceEntitiesForAccountCurrencyCategoryTransactionAndTransactionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Currencies",
                comment: "The Account Currency");

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Transaction's GUID"),
                    TransactionType = table.Column<int>(type: "int", nullable: false, comment: "Transaction Type Enum"),
                    Amount = table.Column<decimal>(type: "decimal(14,2)", nullable: false, comment: "The Amount of the Transaction"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Time When Transaction Is Created"),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Time When Transaction Is Modified For Last Time"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "User ID of Creator of Transaction"),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The Account In Which the Transaction Was Made")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Transaction Performed On an Account");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_AccountId",
                table: "Transaction",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserId",
                table: "Transaction",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.AlterTable(
                name: "Currencies",
                oldComment: "The Account Currency");
        }
    }
}
