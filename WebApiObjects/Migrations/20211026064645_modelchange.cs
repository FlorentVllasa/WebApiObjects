using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiObjects.Migrations
{
    public partial class modelchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Tags_TagID",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_TagID",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "TagID",
                table: "Models");

            migrationBuilder.AddColumn<Guid>(
                name: "ModelID",
                table: "Tags",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ModelID",
                table: "Tags",
                column: "ModelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Models_ModelID",
                table: "Tags",
                column: "ModelID",
                principalTable: "Models",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Models_ModelID",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ModelID",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ModelID",
                table: "Tags");

            migrationBuilder.AddColumn<Guid>(
                name: "TagID",
                table: "Models",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Models_TagID",
                table: "Models",
                column: "TagID");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Tags_TagID",
                table: "Models",
                column: "TagID",
                principalTable: "Tags",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
