using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPrivateLibraryAPI.Migrations
{
    public partial class ChangeUserBookKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserBooks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserBooks",
                nullable: false,
                defaultValue: 0);
        }
    }
}
