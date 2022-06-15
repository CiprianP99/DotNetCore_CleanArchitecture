using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreClean.Infra.Data.Migrations
{
    public partial class fourthMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InitiatorUserId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_InitiatorUserId",
                table: "Notifications",
                column: "InitiatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_InitiatorUserId",
                table: "Notifications",
                column: "InitiatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_InitiatorUserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_InitiatorUserId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "InitiatorUserId",
                table: "Notifications");
        }
    }
}
