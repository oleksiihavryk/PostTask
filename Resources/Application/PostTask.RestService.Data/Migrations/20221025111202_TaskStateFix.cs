using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostTask.RestService.Data.Migrations
{
    public partial class TaskStateFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_ItemState_StateId",
                table: "Task");

            migrationBuilder.AlterColumn<Guid>(
                name: "StateId",
                table: "Task",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_ItemState_StateId",
                table: "Task",
                column: "StateId",
                principalTable: "ItemState",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_ItemState_StateId",
                table: "Task");

            migrationBuilder.AlterColumn<Guid>(
                name: "StateId",
                table: "Task",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_ItemState_StateId",
                table: "Task",
                column: "StateId",
                principalTable: "ItemState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
