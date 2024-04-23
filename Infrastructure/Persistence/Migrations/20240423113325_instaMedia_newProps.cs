using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EssaAPI.Migrations
{
    /// <inheritdoc />
    public partial class instaMedia_newProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsGalleryItem",
                table: "InstaMedias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ParentInstaId",
                table: "InstaMedias",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGalleryItem",
                table: "InstaMedias");

            migrationBuilder.DropColumn(
                name: "ParentInstaId",
                table: "InstaMedias");
        }
    }
}
