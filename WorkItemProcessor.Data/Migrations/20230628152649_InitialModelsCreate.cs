using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkItemProcessor.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialModelsCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniqueId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniqueName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descriptor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkItemRevisions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamProject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IterationPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkItemType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    ChangedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedById = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItemRevisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkItemRevisions_Users_ChangedById",
                        column: x => x.ChangedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkItemRevisions_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkItemRevisions_ChangedById",
                table: "WorkItemRevisions",
                column: "ChangedById");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItemRevisions_CreatedById",
                table: "WorkItemRevisions",
                column: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkItemRevisions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
