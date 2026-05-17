using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SponsorshipWorkflow.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRequestorIdToString2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "RequestId",
                table: "RequestWorkflowHistories",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ActionByUserId",
                table: "RequestWorkflowHistories",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestId",
                table: "RequestWorkflowHistories",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "ActionByUserId",
                table: "RequestWorkflowHistories",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
