using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace artPost_.Data.Migrations
{
    /// <inheritdoc />
    public partial class initiald : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "useList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_useList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    profilePicLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    followers = table.Column<int>(type: "int", nullable: false),
                    userListId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_useList_userListId",
                        column: x => x.userListId,
                        principalTable: "useList",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_userListId",
                table: "user",
                column: "userListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "useList");
        }
    }
}
