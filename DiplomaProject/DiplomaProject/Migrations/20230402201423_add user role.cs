using DiplomaProject.EntityModels.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaProject.Migrations
{
    /// <inheritdoc />
    public partial class adduserrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: UserRole.BaseUser);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");
        }
    }
}
