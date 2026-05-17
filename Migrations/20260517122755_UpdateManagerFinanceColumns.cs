using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SponsorshipWorkflow.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateManagerFinanceColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectedByFinance",
                table: "SponsorshipRequests");

            migrationBuilder.DropColumn(
                name: "RejectedByManager",
                table: "SponsorshipRequests");

            migrationBuilder.RenameColumn(
                name: "RejectedManagerId",
                table: "SponsorshipRequests",
                newName: "ManagerId");

            migrationBuilder.RenameColumn(
                name: "RejectedFinanceId",
                table: "SponsorshipRequests",
                newName: "FinanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "SponsorshipRequests",
                newName: "RejectedManagerId");

            migrationBuilder.RenameColumn(
                name: "FinanceId",
                table: "SponsorshipRequests",
                newName: "RejectedFinanceId");

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
        }
    }
}
