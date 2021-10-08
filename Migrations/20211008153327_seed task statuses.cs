using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManager.Migrations
{
    public partial class seedtaskstatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Identity",
                table: "Task");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                schema: "Identity",
                table: "Task",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Status",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[] { 0, "NEW" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "IN_PROGRESS" });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "FINISHED" });

            migrationBuilder.CreateIndex(
                name: "IX_Task_StatusId",
                schema: "Identity",
                table: "Task",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Status_StatusId",
                schema: "Identity",
                table: "Task",
                column: "StatusId",
                principalSchema: "Identity",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Status_StatusId",
                schema: "Identity",
                table: "Task");

            migrationBuilder.DropTable(
                name: "Status",
                schema: "Identity");

            migrationBuilder.DropIndex(
                name: "IX_Task_StatusId",
                schema: "Identity",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "StatusId",
                schema: "Identity",
                table: "Task");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "Identity",
                table: "Task",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
