using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileService.Infrastructure.Migrations
{
    public partial class AddUpload2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_urls_uploads_UploadItemId",
                table: "urls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_urls",
                table: "urls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_uploads",
                table: "uploads");

            migrationBuilder.RenameTable(
                name: "urls",
                newName: "T_UploadUrl");

            migrationBuilder.RenameTable(
                name: "uploads",
                newName: "T_UploadItem");

            migrationBuilder.RenameIndex(
                name: "IX_urls_UploadItemId",
                table: "T_UploadUrl",
                newName: "IX_T_UploadUrl_UploadItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_UploadUrl",
                table: "T_UploadUrl",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_UploadItem",
                table: "T_UploadItem",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_T_UploadUrl_T_UploadItem_UploadItemId",
                table: "T_UploadUrl",
                column: "UploadItemId",
                principalTable: "T_UploadItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_UploadUrl_T_UploadItem_UploadItemId",
                table: "T_UploadUrl");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_UploadUrl",
                table: "T_UploadUrl");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_UploadItem",
                table: "T_UploadItem");

            migrationBuilder.RenameTable(
                name: "T_UploadUrl",
                newName: "urls");

            migrationBuilder.RenameTable(
                name: "T_UploadItem",
                newName: "uploads");

            migrationBuilder.RenameIndex(
                name: "IX_T_UploadUrl_UploadItemId",
                table: "urls",
                newName: "IX_urls_UploadItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_urls",
                table: "urls",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_uploads",
                table: "uploads",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_urls_uploads_UploadItemId",
                table: "urls",
                column: "UploadItemId",
                principalTable: "uploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
