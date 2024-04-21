using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EssaAPI.Migrations
{
    /// <inheritdoc />
    public partial class rentRequest_mail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InformationMailSent",
                table: "RentRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InformationMailSent",
                table: "RentRequests");
        }
    }
}
