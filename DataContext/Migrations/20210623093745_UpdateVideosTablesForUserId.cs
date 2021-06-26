using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class UpdateVideosTablesForUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Video_AspNetUsers_UsersId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Video_UsersId",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Video");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Video",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Video_UserId",
                table: "Video",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Video_AspNetUsers_UserId",
                table: "Video",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Video_AspNetUsers_UserId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Video_UserId",
                table: "Video");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Video",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "Video",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Video_UsersId",
                table: "Video",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Video_AspNetUsers_UsersId",
                table: "Video",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
