using Microsoft.EntityFrameworkCore.Migrations;

namespace SharingService.Data.EntityFramework.SqlServer.Migrations
{
    public partial class Anchors_AddKeyColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "key",
                table: "anchors",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "key",
                table: "anchors");
        }
    }
}
