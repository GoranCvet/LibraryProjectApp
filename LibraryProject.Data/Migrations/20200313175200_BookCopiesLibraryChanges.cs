using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryProject.Data.Migrations
{
    public partial class BookCopiesLibraryChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LibriryCopies",
                table: "BookCopiesLibraries",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LibriryCopies",
                table: "BookCopiesLibraries");
        }
    }
}
