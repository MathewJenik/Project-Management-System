using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Management_System.Migrations
{
    public partial class implement_project_notes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "projects");
        }
    }
}
