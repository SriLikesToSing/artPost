using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace artPost_.Data.Migrations
{
    /// <inheritdoc />
    public partial class jsonString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_user_userId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_userId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Image");

            migrationBuilder.AddColumn<string>(
                name: "imagesJsonString",
                table: "user",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagesJsonString",
                table: "user");

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Image",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_userId",
                table: "Image",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_user_userId",
                table: "Image",
                column: "userId",
                principalTable: "user",
                principalColumn: "Id");
        }
    }
}
