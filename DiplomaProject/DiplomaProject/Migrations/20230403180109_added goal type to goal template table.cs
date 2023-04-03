using DiplomaProject.DataSeeding;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaProject.Migrations
{
    /// <inheritdoc />
    public partial class addedgoaltypetogoaltemplatetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoalType",
                table: "GoalTemplates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            //FillTables(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoalType",
                table: "GoalTemplates");
        }

        //private void FillTables(MigrationBuilder migrationBuilder)
        //{
        //    var templates = GoalDescriptionInitialData.GoalTemplatesInitialData;
        //    foreach (var template in templates)
        //    {
        //        migrationBuilder.Sql($"insert into public.\"GoalTemplates\"(\"Id\", \"GoalType\", \"Description\") values ('{template.Id}', '{template.GoalType}', {template.Description.ToString()})");
        //    }
        //}
    }
}
