using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonKey.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedAccountTransactionBusinessLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Accounts_AccountId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_AspNetUsers_UserId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_UserId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Transaction");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_AccountId",
                table: "Transactions",
                newName: "IX_Transactions_AccountId");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Accounts",
                type: "decimal(14,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "The Balance of the Account");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionType",
                table: "Transactions",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "Transaction Type Enum",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Transaction Type Enum");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(14,2)",
                nullable: false,
                comment: "The amount in the account's currency; balance = SUM(Amount)",
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldComment: "The Amount of the Transaction");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Category (null for transfers)");

            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeRate",
                table: "Transactions",
                type: "decimal(18,8)",
                nullable: true,
                comment: "Exchange rate used for a cross-currency transfer (source → destination); null otherwise");

            migrationBuilder.AddColumn<Guid>(
                name: "TransferId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Shared by both legs of a transfer; null otherwise");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Category's GUID"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Category's Name"),
                    IconUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false, comment: "URL to Category's Icon")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                },
                comment: "The Category of the Transaction Performed");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransferId",
                table: "Transactions",
                column: "TransferId");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Transaction_Amount",
                table: "Transactions",
                sql: "([TransactionType] <> 'Income' OR [Amount] > 0) AND ([TransactionType] <> 'Outcome' OR [Amount] < 0) AND [Amount] <> 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Transaction_Transfer",
                table: "Transactions",
                sql: "([TransactionType] = 'Transfer' AND [TransferId] IS NOT NULL AND [CategoryId] IS NULL) OR ([TransactionType] <> 'Transfer' AND [TransferId] IS NULL AND [ExchangeRate] IS NULL)");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TransferId",
                table: "Transactions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Transaction_Amount",
                table: "Transactions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Transaction_Transfer",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ExchangeRate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransferId",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transaction");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_AccountId",
                table: "Transaction",
                newName: "IX_Transaction_AccountId");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Accounts",
                type: "decimal(14,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "The Amount of the Account");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionType",
                table: "Transaction",
                type: "int",
                nullable: false,
                comment: "Transaction Type Enum",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "Transaction Type Enum");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transaction",
                type: "decimal(14,2)",
                nullable: false,
                comment: "The Amount of the Transaction",
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldComment: "The amount in the account's currency; balance = SUM(Amount)");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "User ID of Creator of Transaction");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserId",
                table: "Transaction",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Accounts_AccountId",
                table: "Transaction",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_AspNetUsers_UserId",
                table: "Transaction",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
