using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryProject.Data.Migrations
{
    public partial class LendingsAndLibrary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LibraryId",
                table: "Lendings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lendings_LibraryId",
                table: "Lendings",
                column: "LibraryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lendings_Libraries_LibraryId",
                table: "Lendings",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lendings_Libraries_LibraryId",
                table: "Lendings");

            migrationBuilder.DropIndex(
                name: "IX_Lendings_LibraryId",
                table: "Lendings");

            migrationBuilder.DropColumn(
                name: "LibraryId",
                table: "Lendings");
        }
    }
}
