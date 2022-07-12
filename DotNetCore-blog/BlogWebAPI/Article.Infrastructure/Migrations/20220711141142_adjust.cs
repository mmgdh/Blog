using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArticleService.Infrastructure.Migrations
{
    public partial class adjust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClassifyText",
                table: "T_ArticleClassify",
                newName: "PinYin");

            migrationBuilder.AlterColumn<Guid>(
                name: "DefaultImgId",
                table: "T_ArticleClassify",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "ClassifyName",
                table: "T_ArticleClassify",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassifyName",
                table: "T_ArticleClassify");

            migrationBuilder.RenameColumn(
                name: "PinYin",
                table: "T_ArticleClassify",
                newName: "ClassifyText");

            migrationBuilder.AlterColumn<Guid>(
                name: "DefaultImgId",
                table: "T_ArticleClassify",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
