using Microsoft.EntityFrameworkCore.Migrations;

namespace ADSBackend.Migrations
{
    public partial class addedCategoriesToTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Tag",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_CategoryId",
                table: "Tag",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Category_CategoryId",
                table: "Tag",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Category_CategoryId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_CategoryId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Tag");
        }
    }
}
