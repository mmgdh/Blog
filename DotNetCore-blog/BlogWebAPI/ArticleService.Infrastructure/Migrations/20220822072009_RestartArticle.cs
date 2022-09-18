using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArticleService.Infrastructure.Migrations
{
    public partial class RestartArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_ArticleClassify",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassifyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PinYin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultImgId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_ArticleClassify", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_ArticleTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PinYin = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_ArticleTags", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "T_Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassifyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PinYin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Articles", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_T_Articles_T_ArticleClassify_ClassifyId",
                        column: x => x.ClassifyId,
                        principalTable: "T_ArticleClassify",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_ArticleContent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_ArticleContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_ArticleContent_T_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "T_Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Articles_Tags",
                columns: table => new
                {
                    ArticlesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Articles_Tags", x => new { x.ArticlesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_T_Articles_Tags_T_Articles_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "T_Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_Articles_Tags_T_ArticleTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "T_ArticleTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_ArticleContent_ArticleId",
                table: "T_ArticleContent",
                column: "ArticleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Articles_ClassifyId",
                table: "T_Articles",
                column: "ClassifyId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Articles_Tags_TagsId",
                table: "T_Articles_Tags",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_ArticleContent");

            migrationBuilder.DropTable(
                name: "T_Articles_Tags");

            migrationBuilder.DropTable(
                name: "T_Articles");

            migrationBuilder.DropTable(
                name: "T_ArticleTags");

            migrationBuilder.DropTable(
                name: "T_ArticleClassify");
        }
    }
}
