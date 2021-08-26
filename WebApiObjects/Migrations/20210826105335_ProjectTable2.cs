using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiObjects.Migrations
{
    public partial class ProjectTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Project_ParentProjectID",
                table: "Models");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Project_ProjectID",
                table: "Models");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Projects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Projects_ParentProjectID",
                table: "Models",
                column: "ParentProjectID",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Projects_ProjectID",
                table: "Models",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Projects_ParentProjectID",
                table: "Models");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Projects_ProjectID",
                table: "Models");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Project");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Project_ParentProjectID",
                table: "Models",
                column: "ParentProjectID",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Project_ProjectID",
                table: "Models",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
