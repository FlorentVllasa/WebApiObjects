using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiObjects.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_ModelType_ModelTypeID",
                table: "Models");

            migrationBuilder.DropForeignKey(
                name: "FK_Type_ModelType_ModelTypeID",
                table: "Type");

            migrationBuilder.DropForeignKey(
                name: "FK_Type_ModelType_ParentModelTypeId",
                table: "Type");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Type",
                table: "Type");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModelType",
                table: "ModelType");

            migrationBuilder.RenameTable(
                name: "Type",
                newName: "Types");

            migrationBuilder.RenameTable(
                name: "ModelType",
                newName: "ModelTypes");

            migrationBuilder.RenameIndex(
                name: "IX_Type_ParentModelTypeId",
                table: "Types",
                newName: "IX_Types_ParentModelTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Type_ModelTypeID",
                table: "Types",
                newName: "IX_Types_ModelTypeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Types",
                table: "Types",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModelTypes",
                table: "ModelTypes",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_ModelTypes_ModelTypeID",
                table: "Models",
                column: "ModelTypeID",
                principalTable: "ModelTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Types_ModelTypes_ModelTypeID",
                table: "Types",
                column: "ModelTypeID",
                principalTable: "ModelTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Types_ModelTypes_ParentModelTypeId",
                table: "Types",
                column: "ParentModelTypeId",
                principalTable: "ModelTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_ModelTypes_ModelTypeID",
                table: "Models");

            migrationBuilder.DropForeignKey(
                name: "FK_Types_ModelTypes_ModelTypeID",
                table: "Types");

            migrationBuilder.DropForeignKey(
                name: "FK_Types_ModelTypes_ParentModelTypeId",
                table: "Types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Types",
                table: "Types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModelTypes",
                table: "ModelTypes");

            migrationBuilder.RenameTable(
                name: "Types",
                newName: "Type");

            migrationBuilder.RenameTable(
                name: "ModelTypes",
                newName: "ModelType");

            migrationBuilder.RenameIndex(
                name: "IX_Types_ParentModelTypeId",
                table: "Type",
                newName: "IX_Type_ParentModelTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Types_ModelTypeID",
                table: "Type",
                newName: "IX_Type_ModelTypeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Type",
                table: "Type",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModelType",
                table: "ModelType",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_ModelType_ModelTypeID",
                table: "Models",
                column: "ModelTypeID",
                principalTable: "ModelType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Type_ModelType_ModelTypeID",
                table: "Type",
                column: "ModelTypeID",
                principalTable: "ModelType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Type_ModelType_ParentModelTypeId",
                table: "Type",
                column: "ParentModelTypeId",
                principalTable: "ModelType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
