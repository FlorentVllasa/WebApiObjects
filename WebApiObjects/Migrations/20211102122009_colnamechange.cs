using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiObjects.Migrations
{
    public partial class colnamechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "text",
                table: "Models",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Models",
                newName: "text");
        }
    }
}
