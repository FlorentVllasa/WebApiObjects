using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiObjects.Migrations
{
    public partial class ProjectTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModelID",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentProjectID",
                table: "Models",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectID",
                table: "Models",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ModelID",
                table: "Properties",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Models_ParentProjectID",
                table: "Models",
                column: "ParentProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Models_ProjectID",
                table: "Models",
                column: "ProjectID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Models_ModelID",
                table: "Properties",
                column: "ModelID",
                principalTable: "Models",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Project_ParentProjectID",
                table: "Models");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Project_ProjectID",
                table: "Models");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Models_ModelID",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Properties_ModelID",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Models_ParentProjectID",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_ProjectID",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "ModelID",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ParentProjectID",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "Models");
        }
    }
}
