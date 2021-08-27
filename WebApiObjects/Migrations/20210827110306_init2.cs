using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiObjects.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentModelID",
                table: "Models",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Models_ParentModelID",
                table: "Models",
                column: "ParentModelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Models_ParentModelID",
                table: "Models",
                column: "ParentModelID",
                principalTable: "Models",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Models_ParentModelID",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_ParentModelID",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "ParentModelID",
                table: "Models");
        }
    }
}
