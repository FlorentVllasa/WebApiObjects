using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiObjects.Migrations
{
    public partial class TableChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Models_SubModelID",
                table: "Models");

            migrationBuilder.RenameColumn(
                name: "SubModelID",
                table: "Models",
                newName: "ModelID");

            migrationBuilder.RenameIndex(
                name: "IX_Models_SubModelID",
                table: "Models",
                newName: "IX_Models_ModelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Models_ModelID",
                table: "Models",
                column: "ModelID",
                principalTable: "Models",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Models_ModelID",
                table: "Models");

            migrationBuilder.RenameColumn(
                name: "ModelID",
                table: "Models",
                newName: "SubModelID");

            migrationBuilder.RenameIndex(
                name: "IX_Models_ModelID",
                table: "Models",
                newName: "IX_Models_SubModelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Models_SubModelID",
                table: "Models",
                column: "SubModelID",
                principalTable: "Models",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
