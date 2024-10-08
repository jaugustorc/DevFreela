using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevFreela.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOldFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComments_Users_UserId",
                table: "ProjectComments");

            migrationBuilder.DropIndex(
                name: "IX_ProjectComments_UserId",
                table: "ProjectComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProjectComments");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectComments_IdUser",
                table: "ProjectComments",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComments_Users_IdUser",
                table: "ProjectComments",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComments_Users_IdUser",
                table: "ProjectComments");

            migrationBuilder.DropIndex(
                name: "IX_ProjectComments_IdUser",
                table: "ProjectComments");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ProjectComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectComments_UserId",
                table: "ProjectComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComments_Users_UserId",
                table: "ProjectComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
