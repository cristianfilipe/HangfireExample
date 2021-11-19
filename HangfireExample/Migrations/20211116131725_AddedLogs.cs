using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HangfireExample.Migrations
{
    public partial class AddedLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "ProcessingQueue",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProcessingQueueLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogType = table.Column<int>(type: "int", nullable: false),
                    ProcessingQueueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessingQueueLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessingQueueLog_ProcessingQueue_ProcessingQueueId",
                        column: x => x.ProcessingQueueId,
                        principalTable: "ProcessingQueue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessingQueueLog_ProcessingQueueId",
                table: "ProcessingQueueLog",
                column: "ProcessingQueueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessingQueueLog");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "ProcessingQueue");
        }
    }
}
