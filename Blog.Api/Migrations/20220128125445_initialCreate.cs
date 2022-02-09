﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Api.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "MainCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    MainCategoryName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 1, 28, 12, 54, 45, 519, DateTimeKind.Utc).AddTicks(3755)),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 1, 28, 12, 54, 45, 519, DateTimeKind.Utc).AddTicks(3982)),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    UserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UserSurname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NickName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Gender = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(7988)),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(8216)),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    MainCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TagName = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(5294)),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(5491)),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_MainCategories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "MainCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000")),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Content = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: false),
                    Thumbnail = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true, defaultValue: "default.jpg"),
                    ViewsCount = table.Column<int>(type: "integer", nullable: false),
                    CommentCount = table.Column<int>(type: "integer", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(991)),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(1217)),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000")),
                    ArticleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 1, 28, 12, 54, 45, 519, DateTimeKind.Utc).AddTicks(372)),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 1, 28, 12, 54, 45, 519, DateTimeKind.Utc).AddTicks(612)),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MainCategories",
                columns: new[] { "Id", "CreatedDate", "IsActive", "MainCategoryName", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("11070708-1c30-4967-9bcf-433e703f348a"), new DateTime(2022, 1, 28, 12, 54, 45, 519, DateTimeKind.Utc).AddTicks(4626), true, "Kültür", new DateTime(2022, 1, 28, 12, 54, 45, 519, DateTimeKind.Utc).AddTicks(4627) },
                    { new Guid("eec3877e-de06-47a5-9f29-764cebf7851d"), new DateTime(2022, 1, 28, 12, 54, 45, 519, DateTimeKind.Utc).AddTicks(4617), true, "Bilim", new DateTime(2022, 1, 28, 12, 54, 45, 519, DateTimeKind.Utc).AddTicks(4622) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "CreatedDate", "Email", "IsActive", "IsApproved", "NickName", "Password", "UpdatedDate", "UserName", "UserSurname" },
                values: new object[] { new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c"), new DateTime(1990, 11, 17, 22, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(9861), "ömer@ömer.com", true, true, "ömergürsoy", "$2a$11$uNx/XA0odP6BAp8xKqtkausOYVPqmGNmq1GYK/y0E6OgQNb/7XIfC", new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(9863), "Ömer", "Gürsoy" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "CreatedDate", "Email", "Gender", "IsActive", "IsApproved", "NickName", "Password", "UpdatedDate", "UserName", "UserSurname" },
                values: new object[] { new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908"), new DateTime(1995, 12, 27, 22, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(9775), "necil@necil.com", true, true, true, "necilcakmak", "$2a$11$wnQMJKF1vC6fAxs5IDaM1.5S3oMG.gEQMhON0bHUl5UQfe8v1AwIK", new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(9782), "Necil", "Çakmak" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "Description", "IsActive", "MainCategoryId", "TagName", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("1bbc4e68-3e73-4f11-bd09-11ba71b5b582"), "Sinema", new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(7462), "Sinema kategorisine ait makaleler", true, new Guid("11070708-1c30-4967-9bcf-433e703f348a"), "SİN", new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(7464) },
                    { new Guid("5533e9a6-186f-4a3d-9ef4-63a2f7c02eb2"), "Yazılım", new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(7449), "Yazılım kategorisine ait makaleler", true, new Guid("eec3877e-de06-47a5-9f29-764cebf7851d"), "YZL", new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(7457) }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedDate", "Description", "IsActive", "PublishedDate", "Thumbnail", "Title", "UpdatedDate", "UserId", "ViewsCount" },
                values: new object[,]
                {
                    { new Guid("15cd7fe9-3d73-4028-b3f1-0e8a09112570"), new Guid("5533e9a6-186f-4a3d-9ef4-63a2f7c02eb2"), 3, "ikinci makalenin içeriği", new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(4578), "ikinci makale açıklaması", true, new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(4583), "default.jpg", "ikinci makale", new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(4580), new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c"), 25 },
                    { new Guid("507462a3-5639-4573-b7d9-306d560a7ca8"), new Guid("5533e9a6-186f-4a3d-9ef4-63a2f7c02eb2"), 2, "ilk makalenin içeriği", new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(4562), "ilk makale açıklaması", true, new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(4574), "default.jpg", "ilk makale", new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(4570), new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908"), 33 },
                    { new Guid("d1267b3b-c386-4481-804b-17c38c28d122"), new Guid("1bbc4e68-3e73-4f11-bd09-11ba71b5b582"), 1, "üçüncü makalenin içeriği", new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(4586), "üçüncü makale açıklaması", true, new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(4590), "default.jpg", "üçüncü makale", new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(4587), new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c"), 11 },
                    { new Guid("ddb5c34f-518c-4189-ae3a-fe9103558500"), new Guid("1bbc4e68-3e73-4f11-bd09-11ba71b5b582"), 5, "dördüncü makalenin içeriği", new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(4593), "dördüncü makale açıklaması", true, new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(4597), "default.jpg", "dördüncü makale", new DateTime(2022, 1, 28, 12, 54, 45, 518, DateTimeKind.Utc).AddTicks(4594), new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908"), 10 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CreatedDate", "IsActive", "Text", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { new Guid("c8d2fc77-9c77-48fe-9e7b-4f47c34fe27e"), new Guid("507462a3-5639-4573-b7d9-306d560a7ca8"), new DateTime(2022, 1, 28, 12, 54, 45, 519, DateTimeKind.Utc).AddTicks(3245), true, "örnek yorum 1", new DateTime(2022, 1, 28, 12, 54, 45, 519, DateTimeKind.Utc).AddTicks(3252), new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908") },
                    { new Guid("e8acb53c-0f5d-44c6-bc2d-14f2afce41c7"), new Guid("15cd7fe9-3d73-4028-b3f1-0e8a09112570"), new DateTime(2022, 1, 28, 12, 54, 45, 519, DateTimeKind.Utc).AddTicks(3257), true, "örnek yorum 3", new DateTime(2022, 1, 28, 12, 54, 45, 519, DateTimeKind.Utc).AddTicks(3259), new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c") },
                    { new Guid("fcda26c7-2469-415f-b2bf-7b2571c11e4a"), new Guid("d1267b3b-c386-4481-804b-17c38c28d122"), new DateTime(2022, 1, 28, 12, 54, 45, 519, DateTimeKind.Utc).AddTicks(3263), true, "örnek yorum 2", new DateTime(2022, 1, 28, 12, 54, 45, 519, DateTimeKind.Utc).AddTicks(3264), new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserId",
                table: "Articles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_MainCategoryId",
                table: "Categories",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MainCategories_MainCategoryName",
                table: "MainCategories",
                column: "MainCategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_NickName",
                table: "Users",
                column: "NickName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "MainCategories");
        }
    }
}