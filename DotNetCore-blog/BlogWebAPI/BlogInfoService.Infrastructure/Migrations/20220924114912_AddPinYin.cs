using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogInfoService.Infrastructure.Migrations
{
    public partial class AddPinYin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PinYin",
                table: "T_BlogParameter",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PinYin",
                table: "T_BlogParameter");
        }
    }
}
