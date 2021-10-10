using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManager.Migrations
{
    public partial class projectmanagerforeignkeyadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ManagerId",
                schema: "Identity",
                table: "Project",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_ManagerId",
                schema: "Identity",
                table: "Project",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_ManagerId",
                schema: "Identity",
                table: "Project",
                column: "ManagerId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_ManagerId",
                schema: "Identity",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_ManagerId",
                schema: "Identity",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                schema: "Identity",
                table: "Project");
        }
    }
}
