using Microsoft.EntityFrameworkCore.Migrations;

namespace ADSBackend.Migrations
{
    public partial class AddedNickname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "Member",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "Member");
        }
    }
}
