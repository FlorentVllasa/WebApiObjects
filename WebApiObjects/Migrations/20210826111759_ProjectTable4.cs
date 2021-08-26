using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiObjects.Migrations
{
    public partial class ProjectTable4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Projects_ParentProjectID",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_ParentProjectID",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "ParentProjectID",
                table: "Models");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentProjectID",
                table: "Models",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Models_ParentProjectID",
                table: "Models",
                column: "ParentProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Projects_ParentProjectID",
                table: "Models",
                column: "ParentProjectID",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
