using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManager.Migrations
{
    public partial class addmodelclasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SurName",
                schema: "Identity",
                table: "User",
                newName: "Surname");

            migrationBuilder.CreateTable(
                name: "Task",
                schema: "Identity",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectCode = table.Column<int>(type: "int", nullable: true),
                    AssigneeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Progress = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Task_Project_ProjectCode",
                        column: x => x.ProjectCode,
                        principalSchema: "Identity",
                        principalTable: "Project",
                        principalColumn: "ProjectCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Task_User_AssigneeId",
                        column: x => x.AssigneeId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_AssigneeId",
                schema: "Identity",
                table: "Task",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_ProjectCode",
                schema: "Identity",
                table: "Task",
                column: "ProjectCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task",
                schema: "Identity");

            migrationBuilder.RenameColumn(
                name: "Surname",
                schema: "Identity",
                table: "User",
                newName: "SurName");
        }
    }
}
