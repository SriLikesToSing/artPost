using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace artPost_.Data.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_useList_userListId",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_useList",
                table: "useList");

            migrationBuilder.DropColumn(
                name: "bruh",
                table: "useList");

            migrationBuilder.RenameTable(
                name: "useList",
                newName: "userList");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userList",
                table: "userList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_userList_userListId",
                table: "user",
                column: "userListId",
                principalTable: "userList",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_userList_userListId",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userList",
                table: "userList");

            migrationBuilder.RenameTable(
                name: "userList",
                newName: "useList");

            migrationBuilder.AddColumn<int>(
                name: "bruh",
                table: "useList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_useList",
                table: "useList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_useList_userListId",
                table: "user",
                column: "userListId",
                principalTable: "useList",
                principalColumn: "Id");
        }
    }
}
