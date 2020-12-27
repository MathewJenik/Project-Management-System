using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Management_System.Migrations
{
    public partial class Fixed_Wrong_DataType_In_ProjTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "projTasks");

            migrationBuilder.AddColumn<int>(
                name: "StatusID",
                table: "projTasks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_projTasks_StatusID",
                table: "projTasks",
                column: "StatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_projTasks_taskStatuses_StatusID",
                table: "projTasks",
                column: "StatusID",
                principalTable: "taskStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projTasks_taskStatuses_StatusID",
                table: "projTasks");

            migrationBuilder.DropIndex(
                name: "IX_projTasks_StatusID",
                table: "projTasks");

            migrationBuilder.DropColumn(
                name: "StatusID",
                table: "projTasks");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "projTasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
