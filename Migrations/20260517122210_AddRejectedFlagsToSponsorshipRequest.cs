using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SponsorshipWorkflow.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddRejectedFlagsToSponsorshipRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RejectedByFinance",
                table: "SponsorshipRequests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RejectedByManager",
                table: "SponsorshipRequests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RejectedFinanceId",
                table: "SponsorshipRequests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RejectedManagerId",
                table: "SponsorshipRequests",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectedByFinance",
                table: "SponsorshipRequests");

            migrationBuilder.DropColumn(
                name: "RejectedByManager",
                table: "SponsorshipRequests");

            migrationBuilder.DropColumn(
                name: "RejectedFinanceId",
                table: "SponsorshipRequests");

            migrationBuilder.DropColumn(
                name: "RejectedManagerId",
                table: "SponsorshipRequests");
        }
    }
}
