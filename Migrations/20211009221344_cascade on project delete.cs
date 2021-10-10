using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManager.Migrations
{
    public partial class cascadeonprojectdelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Project_ProjectCode",
                schema: "Identity",
                table: "Task");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Project_ProjectCode",
                schema: "Identity",
                table: "Task",
                column: "ProjectCode",
                principalSchema: "Identity",
                principalTable: "Project",
                principalColumn: "ProjectCode",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Project_ProjectCode",
                schema: "Identity",
                table: "Task");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Project_ProjectCode",
                schema: "Identity",
                table: "Task",
                column: "ProjectCode",
                principalSchema: "Identity",
                principalTable: "Project",
                principalColumn: "ProjectCode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
