using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class UpdateBlogController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_AspNetUsers_UsersId",
                table: "Blog");

            migrationBuilder.DropIndex(
                name: "IX_Blog_UsersId",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Blog");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Blog",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_UserId",
                table: "Blog",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_AspNetUsers_UserId",
                table: "Blog",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_AspNetUsers_UserId",
                table: "Blog");

            migrationBuilder.DropIndex(
                name: "IX_Blog_UserId",
                table: "Blog");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Blog",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "Blog",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blog_UsersId",
                table: "Blog",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_AspNetUsers_UsersId",
                table: "Blog",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
