using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkItemProcessor.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddWebHookEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "WorkItemRevisions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WebHookEventId",
                table: "WorkItemRevisions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WebHookEvent",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RawData = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebHookEvent", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkItemRevisions_WebHookEventId",
                table: "WorkItemRevisions",
                column: "WebHookEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItemRevisions_WebHookEvent_WebHookEventId",
                table: "WorkItemRevisions",
                column: "WebHookEventId",
                principalTable: "WebHookEvent",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItemRevisions_WebHookEvent_WebHookEventId",
                table: "WorkItemRevisions");

            migrationBuilder.DropTable(
                name: "WebHookEvent");

            migrationBuilder.DropIndex(
                name: "IX_WorkItemRevisions_WebHookEventId",
                table: "WorkItemRevisions");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "WorkItemRevisions");

            migrationBuilder.DropColumn(
                name: "WebHookEventId",
                table: "WorkItemRevisions");
        }
    }
}
