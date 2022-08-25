using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamService.Infrastructure.Migrations
{
    public partial class AddPropertieKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "T_UploadUrl",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "T_UploadUrl");
        }
    }
}
