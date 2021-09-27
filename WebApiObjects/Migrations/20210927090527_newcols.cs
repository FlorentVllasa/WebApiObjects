using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiObjects.Migrations
{
    public partial class newcols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ModelTypeID",
                table: "Models",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ModelType",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Typ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Type_ModelType_ModelTypeID",
                        column: x => x.ModelTypeID,
                        principalTable: "ModelType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Models_ModelTypeID",
                table: "Models",
                column: "ModelTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Type_ModelTypeID",
                table: "Type",
                column: "ModelTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_ModelType_ModelTypeID",
                table: "Models",
                column: "ModelTypeID",
                principalTable: "ModelType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_ModelType_ModelTypeID",
                table: "Models");

            migrationBuilder.DropTable(
                name: "Type");

            migrationBuilder.DropTable(
                name: "ModelType");

            migrationBuilder.DropIndex(
                name: "IX_Models_ModelTypeID",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "ModelTypeID",
                table: "Models");
        }
    }
}
