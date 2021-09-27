using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiObjects.Migrations
{
    public partial class newcols2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentModelTypeId",
                table: "Type",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Type_ParentModelTypeId",
                table: "Type",
                column: "ParentModelTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Type_ModelType_ParentModelTypeId",
                table: "Type",
                column: "ParentModelTypeId",
                principalTable: "ModelType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Type_ModelType_ParentModelTypeId",
                table: "Type");

            migrationBuilder.DropIndex(
                name: "IX_Type_ParentModelTypeId",
                table: "Type");

            migrationBuilder.DropColumn(
                name: "ParentModelTypeId",
                table: "Type");
        }
    }
}
