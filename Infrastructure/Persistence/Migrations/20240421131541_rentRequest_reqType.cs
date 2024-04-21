using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EssaAPI.Migrations
{
    /// <inheritdoc />
    public partial class rentRequest_reqType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestType",
                table: "RentRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestType",
                table: "RentRequests");
        }
    }
}
