using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonKey.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAmountToAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Accounts",
                type: "decimal(14,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "The Amount of the Account");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Accounts");
        }
    }
}
