using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogInfoService.Infrastructure.Migrations
{
    public partial class AddParamType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParamType",
                table: "T_BlogParameter",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParamType",
                table: "T_BlogParameter");
        }
    }
}
