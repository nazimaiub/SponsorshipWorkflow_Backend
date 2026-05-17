using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SponsorshipWorkflow.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRequestorIdToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestId",
                table: "RequestWorkflowHistories",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "RequestId",
                table: "RequestWorkflowHistories",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
