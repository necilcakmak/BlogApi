using System;
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
                name: "ParentCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(856)),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(1068)),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Gender = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    RoleName = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(4860)),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(5034)),
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
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TagName = table.Column<string>(type: "text", nullable: true),
                    MainCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(872)),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(1074)),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_ParentCategories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "ParentCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    ReceiveMail = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    NewBlog = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(2316)),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(2509)),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSettings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
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
                    Content = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: false),
                    Thumbnail = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true, defaultValue: "default.jpg"),
                    Slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Keywords = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    LikeCount = table.Column<int>(type: "integer", nullable: false),
                    CommentCount = table.Column<int>(type: "integer", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 3, 19, 20, 21, 6, 228, DateTimeKind.Utc).AddTicks(5872)),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 3, 19, 20, 21, 6, 228, DateTimeKind.Utc).AddTicks(6195)),
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
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(7211)),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(7548)),
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
                table: "ParentCategories",
                columns: new[] { "Id", "CreatedDate", "IsActive", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("11070708-1c30-4967-9bcf-433e703f348a"), new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(1741), true, "Kültür", new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(1743) },
                    { new Guid("eec3877e-de06-47a5-9f29-764cebf7851d"), new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(1732), true, "Bilim", new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(1738) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "CreatedDate", "Email", "FirstName", "IsActive", "IsApproved", "LastName", "Password", "RoleName", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("30d00d67-4f1e-405f-a992-f9ef825550c8"), new DateTime(1985, 9, 24, 21, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(6569), "server@dogan.com", "Doğan", true, false, "serverdogan", "$2a$11$uNx/XA0odP6BAp8xKqtkausOYVPqmGNmq1GYK/y0E6OgQNb/7XIfC", "User", new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(6571), "Server" },
                    { new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c"), new DateTime(1990, 11, 17, 22, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(6549), "ömer@ömer.com", "Gürsoy", true, false, "ömergürsoy", "$2a$11$uNx/XA0odP6BAp8xKqtkausOYVPqmGNmq1GYK/y0E6OgQNb/7XIfC", "Admin", new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(6555), "Ömer" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "CreatedDate", "Email", "FirstName", "Gender", "IsActive", "IsApproved", "LastName", "Password", "RoleName", "UpdatedDate", "UserName" },
                values: new object[] { new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908"), new DateTime(1995, 12, 27, 22, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(6447), "necil@necil.com", "Çakmak", true, true, false, "necilcakmak", "$2a$11$wnQMJKF1vC6fAxs5IDaM1.5S3oMG.gEQMhON0bHUl5UQfe8v1AwIK", "Admin", new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(6453), "Necil" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "IsActive", "MainCategoryId", "Name", "TagName", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("1bbc4e68-3e73-4f11-bd09-11ba71b5b582"), new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(4244), true, new Guid("11070708-1c30-4967-9bcf-433e703f348a"), "Sinema", "SİN", new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(4246) },
                    { new Guid("5533e9a6-186f-4a3d-9ef4-63a2f7c02eb2"), new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(4230), true, new Guid("eec3877e-de06-47a5-9f29-764cebf7851d"), "Yazılım", "YZL", new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(4238) }
                });

            migrationBuilder.InsertData(
                table: "UserSettings",
                columns: new[] { "Id", "CreatedDate", "IsActive", "NewBlog", "ReceiveMail", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { new Guid("8ff845a2-ad00-4158-8b4a-061a5764c789"), new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(3994), true, true, true, new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(4001), new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908") },
                    { new Guid("a6bacb41-6666-4f2c-b5c8-afdac80f2026"), new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(4012), true, true, true, new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(4014), new Guid("30d00d67-4f1e-405f-a992-f9ef825550c8") },
                    { new Guid("e88c8860-4b9a-4736-9ed3-5cf23a75a86b"), new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(4007), true, true, true, new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(4008), new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c") }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedDate", "IsActive", "Keywords", "LikeCount", "PublishedDate", "Slug", "Thumbnail", "Title", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { new Guid("15cd7fe9-3d73-4028-b3f1-0e8a09112570"), new Guid("5533e9a6-186f-4a3d-9ef4-63a2f7c02eb2"), 3, "ikinci makalenin içeriği", new DateTime(2022, 3, 19, 20, 21, 6, 228, DateTimeKind.Utc).AddTicks(9995), true, "{}", 25, new DateTime(2022, 3, 19, 20, 21, 6, 228, DateTimeKind.Utc).AddTicks(9999), "ikinci-makale", "default.jpg", "ikinci makale", new DateTime(2022, 3, 19, 20, 21, 6, 228, DateTimeKind.Utc).AddTicks(9997), new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c") },
                    { new Guid("507462a3-5639-4573-b7d9-306d560a7ca8"), new Guid("5533e9a6-186f-4a3d-9ef4-63a2f7c02eb2"), 2, "ilk makalenin içeriği", new DateTime(2022, 3, 19, 20, 21, 6, 228, DateTimeKind.Utc).AddTicks(9976), true, "{}", 33, new DateTime(2022, 3, 19, 20, 21, 6, 228, DateTimeKind.Utc).AddTicks(9989), "ilk-makale", "default.jpg", "ilk makale", new DateTime(2022, 3, 19, 20, 21, 6, 228, DateTimeKind.Utc).AddTicks(9985), new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908") },
                    { new Guid("d1267b3b-c386-4481-804b-17c38c28d122"), new Guid("1bbc4e68-3e73-4f11-bd09-11ba71b5b582"), 1, "üçüncü makalenin içeriği", new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(2), true, "{}", 11, new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(6), "ucuncu-makale", "default.jpg", "üçüncü makale", new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(4), new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c") },
                    { new Guid("ddb5c34f-518c-4189-ae3a-fe9103558500"), new Guid("1bbc4e68-3e73-4f11-bd09-11ba71b5b582"), 5, "dördüncü makalenin içeriği", new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(9), true, "{}", 10, new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(13), "dorduncu-makale", "default.jpg", "dördüncü makale", new DateTime(2022, 3, 19, 20, 21, 6, 229, DateTimeKind.Utc).AddTicks(11), new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908") }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CreatedDate", "IsActive", "Text", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { new Guid("c8d2fc77-9c77-48fe-9e7b-4f47c34fe27e"), new Guid("507462a3-5639-4573-b7d9-306d560a7ca8"), new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(310), true, "örnek yorum 1", new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(319), new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908") },
                    { new Guid("e8acb53c-0f5d-44c6-bc2d-14f2afce41c7"), new Guid("15cd7fe9-3d73-4028-b3f1-0e8a09112570"), new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(324), true, "örnek yorum 3", new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(325), new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c") },
                    { new Guid("fcda26c7-2469-415f-b2bf-7b2571c11e4a"), new Guid("d1267b3b-c386-4481-804b-17c38c28d122"), new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(329), true, "örnek yorum 2", new DateTime(2022, 3, 19, 20, 21, 6, 230, DateTimeKind.Utc).AddTicks(330), new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908") }
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
                name: "IX_Categories_MainCategoryId",
                table: "Categories",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentCategories_Name",
                table: "ParentCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_UserId",
                table: "UserSettings",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ParentCategories");
        }
    }
}
