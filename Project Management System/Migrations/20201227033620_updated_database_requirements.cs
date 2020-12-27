using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Project_Management_System.Migrations
{
    public partial class updated_database_requirements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projects_projTasks_TasksID",
                table: "projects");

            migrationBuilder.DropIndex(
                name: "IX_projects_TasksID",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "TasksID",
                table: "projects");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "projTasks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectID",
                table: "projTasks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubTasks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    StatusID = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ProjTaskID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTasks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubTasks_projTasks_ProjTaskID",
                        column: x => x.ProjTaskID,
                        principalTable: "projTasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubTasks_taskStatuses_StatusID",
                        column: x => x.StatusID,
                        principalTable: "taskStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_projTasks_ProjectID",
                table: "projTasks",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_SubTasks_ProjTaskID",
                table: "SubTasks",
                column: "ProjTaskID");

            migrationBuilder.CreateIndex(
                name: "IX_SubTasks_StatusID",
                table: "SubTasks",
                column: "StatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_projTasks_projects_ProjectID",
                table: "projTasks",
                column: "ProjectID",
                principalTable: "projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projTasks_projects_ProjectID",
                table: "projTasks");

            migrationBuilder.DropTable(
                name: "SubTasks");

            migrationBuilder.DropIndex(
                name: "IX_projTasks_ProjectID",
                table: "projTasks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "projTasks");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "projTasks");

            migrationBuilder.AddColumn<int>(
                name: "TasksID",
                table: "projects",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_projects_TasksID",
                table: "projects",
                column: "TasksID");

            migrationBuilder.AddForeignKey(
                name: "FK_projects_projTasks_TasksID",
                table: "projects",
                column: "TasksID",
                principalTable: "projTasks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
