using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonKey.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditAccountDescriptionMaxLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Accounts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "Account's Description (optional)",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true,
                oldComment: "Account's Description (optional)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Accounts",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                comment: "Account's Description (optional)",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "Account's Description (optional)");
        }
    }
}
