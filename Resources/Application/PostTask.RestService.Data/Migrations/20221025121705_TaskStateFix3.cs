using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostTask.RestService.Data.Migrations
{
    public partial class TaskStateFix3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_TaskState_StateId",
                table: "Task");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_TaskState_StateId",
                table: "Task",
                column: "StateId",
                principalTable: "TaskState",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_TaskState_StateId",
                table: "Task");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_TaskState_StateId",
                table: "Task",
                column: "StateId",
                principalTable: "TaskState",
                principalColumn: "Id");
        }
    }
}
