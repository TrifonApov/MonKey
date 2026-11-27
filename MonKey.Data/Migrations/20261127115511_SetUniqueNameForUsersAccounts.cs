using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonKey.Data.Migrations
{
    /// <inheritdoc />
    public partial class SetUniqueNameForUsersAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_OwnerId",
                table: "Accounts");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_OwnerId_Name",
                table: "Accounts",
                columns: new[] { "OwnerId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_OwnerId_Name",
                table: "Accounts");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_OwnerId",
                table: "Accounts",
                column: "OwnerId");
        }
    }
}
