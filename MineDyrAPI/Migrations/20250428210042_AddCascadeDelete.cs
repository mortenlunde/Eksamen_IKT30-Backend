using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MineDyrAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Users_OwnerId",
                schema: "public",
                table: "Animals");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Users_OwnerId",
                schema: "public",
                table: "Animals",
                column: "OwnerId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Users_OwnerId",
                schema: "public",
                table: "Animals");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Users_OwnerId",
                schema: "public",
                table: "Animals",
                column: "OwnerId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
