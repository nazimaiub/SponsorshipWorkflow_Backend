using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SponsorshipWorkflow.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestorRemarksColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestorRemarks",
                table: "SponsorshipRequests",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestorRemarks",
                table: "SponsorshipRequests");
        }
    }
}
