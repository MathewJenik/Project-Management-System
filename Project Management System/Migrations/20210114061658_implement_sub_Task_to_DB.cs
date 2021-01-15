using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Management_System.Migrations
{
    public partial class implement_sub_Task_to_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTasks_projTasks_ProjTaskID",
                table: "SubTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTasks_taskStatuses_StatusID",
                table: "SubTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubTasks",
                table: "SubTasks");

            migrationBuilder.RenameTable(
                name: "SubTasks",
                newName: "subTasks");

            migrationBuilder.RenameIndex(
                name: "IX_SubTasks_StatusID",
                table: "subTasks",
                newName: "IX_subTasks_StatusID");

            migrationBuilder.RenameIndex(
                name: "IX_SubTasks_ProjTaskID",
                table: "subTasks",
                newName: "IX_subTasks_ProjTaskID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_subTasks",
                table: "subTasks",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_subTasks_projTasks_ProjTaskID",
                table: "subTasks",
                column: "ProjTaskID",
                principalTable: "projTasks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_subTasks_taskStatuses_StatusID",
                table: "subTasks",
                column: "StatusID",
                principalTable: "taskStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subTasks_projTasks_ProjTaskID",
                table: "subTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_subTasks_taskStatuses_StatusID",
                table: "subTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_subTasks",
                table: "subTasks");

            migrationBuilder.RenameTable(
                name: "subTasks",
                newName: "SubTasks");

            migrationBuilder.RenameIndex(
                name: "IX_subTasks_StatusID",
                table: "SubTasks",
                newName: "IX_SubTasks_StatusID");

            migrationBuilder.RenameIndex(
                name: "IX_subTasks_ProjTaskID",
                table: "SubTasks",
                newName: "IX_SubTasks_ProjTaskID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubTasks",
                table: "SubTasks",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTasks_projTasks_ProjTaskID",
                table: "SubTasks",
                column: "ProjTaskID",
                principalTable: "projTasks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubTasks_taskStatuses_StatusID",
                table: "SubTasks",
                column: "StatusID",
                principalTable: "taskStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
