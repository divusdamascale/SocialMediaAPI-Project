using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Migrations
{
    /// <inheritdoc />
    public partial class updateLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_UserAccounts_UserId",
                table: "PostLikes");

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_UserAccounts_UserId",
                table: "PostLikes",
                column: "UserId",
                principalTable: "UserAccounts",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_UserAccounts_UserId",
                table: "PostLikes");

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_UserAccounts_UserId",
                table: "PostLikes",
                column: "UserId",
                principalTable: "UserAccounts",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
