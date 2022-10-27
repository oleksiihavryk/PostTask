using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostTask.RestService.Data.Migrations
{
    public partial class TaskStateFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_ItemState_StateId",
                table: "Task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemState",
                table: "ItemState");

            migrationBuilder.RenameTable(
                name: "ItemState",
                newName: "TaskState");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskState",
                table: "TaskState",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_TaskState_StateId",
                table: "Task",
                column: "StateId",
                principalTable: "TaskState",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_TaskState_StateId",
                table: "Task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskState",
                table: "TaskState");

            migrationBuilder.RenameTable(
                name: "TaskState",
                newName: "ItemState");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemState",
                table: "ItemState",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_ItemState_StateId",
                table: "Task",
                column: "StateId",
                principalTable: "ItemState",
                principalColumn: "Id");
        }
    }
}
