using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Api.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(5127),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 822, DateTimeKind.Utc).AddTicks(403));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(4950),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 822, DateTimeKind.Utc).AddTicks(232));

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "MainCategories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 668, DateTimeKind.Utc).AddTicks(966),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(8084));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "MainCategories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 668, DateTimeKind.Utc).AddTicks(781),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(7896));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(7542),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(3787));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(7336),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(3539));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(2433),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(7811));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(2242),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(7621));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Articles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 666, DateTimeKind.Utc).AddTicks(8323),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(3524));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Articles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 666, DateTimeKind.Utc).AddTicks(8103),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(3206));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("15cd7fe9-3d73-4028-b3f1-0e8a09112570"),
                columns: new[] { "CreatedDate", "PublishedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(1554), new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(1558), new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(1555) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("507462a3-5639-4573-b7d9-306d560a7ca8"),
                columns: new[] { "CreatedDate", "PublishedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(1538), new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(1549), new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(1546) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("d1267b3b-c386-4481-804b-17c38c28d122"),
                columns: new[] { "CreatedDate", "PublishedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(1561), new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(1565), new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(1562) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("ddb5c34f-518c-4189-ae3a-fe9103558500"),
                columns: new[] { "CreatedDate", "PublishedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(1568), new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(1572), new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(1569) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1bbc4e68-3e73-4f11-bd09-11ba71b5b582"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(4332), new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(4333) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5533e9a6-186f-4a3d-9ef4-63a2f7c02eb2"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(4317), new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(4326) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("c8d2fc77-9c77-48fe-9e7b-4f47c34fe27e"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 27, 19, 8, 19, 668, DateTimeKind.Utc).AddTicks(126), new DateTime(2022, 1, 27, 19, 8, 19, 668, DateTimeKind.Utc).AddTicks(134) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("e8acb53c-0f5d-44c6-bc2d-14f2afce41c7"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 27, 19, 8, 19, 668, DateTimeKind.Utc).AddTicks(139), new DateTime(2022, 1, 27, 19, 8, 19, 668, DateTimeKind.Utc).AddTicks(141) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("fcda26c7-2469-415f-b2bf-7b2571c11e4a"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 27, 19, 8, 19, 668, DateTimeKind.Utc).AddTicks(145), new DateTime(2022, 1, 27, 19, 8, 19, 668, DateTimeKind.Utc).AddTicks(146) });

            migrationBuilder.UpdateData(
                table: "MainCategories",
                keyColumn: "Id",
                keyValue: new Guid("11070708-1c30-4967-9bcf-433e703f348a"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 27, 19, 8, 19, 668, DateTimeKind.Utc).AddTicks(1569), new DateTime(2022, 1, 27, 19, 8, 19, 668, DateTimeKind.Utc).AddTicks(1570) });

            migrationBuilder.UpdateData(
                table: "MainCategories",
                keyColumn: "Id",
                keyValue: new Guid("eec3877e-de06-47a5-9f29-764cebf7851d"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 27, 19, 8, 19, 668, DateTimeKind.Utc).AddTicks(1559), new DateTime(2022, 1, 27, 19, 8, 19, 668, DateTimeKind.Utc).AddTicks(1566) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c"),
                columns: new[] { "CreatedDate", "IsApproved", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(6638), true, new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(6641) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908"),
                columns: new[] { "CreatedDate", "IsApproved", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(6546), true, new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(6553) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 822, DateTimeKind.Utc).AddTicks(403),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(5127));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 822, DateTimeKind.Utc).AddTicks(232),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(4950));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "MainCategories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(8084),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 668, DateTimeKind.Utc).AddTicks(966));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "MainCategories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(7896),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 668, DateTimeKind.Utc).AddTicks(781));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(3787),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(7542));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(3539),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(7336));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(7811),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(2433));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(7621),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 667, DateTimeKind.Utc).AddTicks(2242));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Articles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(3524),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 666, DateTimeKind.Utc).AddTicks(8323));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Articles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(3206),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2022, 1, 27, 19, 8, 19, 666, DateTimeKind.Utc).AddTicks(8103));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("15cd7fe9-3d73-4028-b3f1-0e8a09112570"),
                columns: new[] { "CreatedDate", "PublishedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(6946), new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(6952), new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(6947) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("507462a3-5639-4573-b7d9-306d560a7ca8"),
                columns: new[] { "CreatedDate", "PublishedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(6929), new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(6941), new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(6937) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("d1267b3b-c386-4481-804b-17c38c28d122"),
                columns: new[] { "CreatedDate", "PublishedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(6955), new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(6959), new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(6956) });

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("ddb5c34f-518c-4189-ae3a-fe9103558500"),
                columns: new[] { "CreatedDate", "PublishedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(6962), new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(6966), new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(6964) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1bbc4e68-3e73-4f11-bd09-11ba71b5b582"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(9701), new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(9703) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5533e9a6-186f-4a3d-9ef4-63a2f7c02eb2"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(9640), new DateTime(2022, 1, 22, 12, 12, 14, 821, DateTimeKind.Utc).AddTicks(9648) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("c8d2fc77-9c77-48fe-9e7b-4f47c34fe27e"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(7356), new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(7365) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("e8acb53c-0f5d-44c6-bc2d-14f2afce41c7"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(7370), new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(7372) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("fcda26c7-2469-415f-b2bf-7b2571c11e4a"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(7376), new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(7377) });

            migrationBuilder.UpdateData(
                table: "MainCategories",
                keyColumn: "Id",
                keyValue: new Guid("11070708-1c30-4967-9bcf-433e703f348a"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(8703), new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(8705) });

            migrationBuilder.UpdateData(
                table: "MainCategories",
                keyColumn: "Id",
                keyValue: new Guid("eec3877e-de06-47a5-9f29-764cebf7851d"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(8695), new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(8700) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(2383), new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(2386) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(2277), new DateTime(2022, 1, 22, 12, 12, 14, 830, DateTimeKind.Utc).AddTicks(2297) });
        }
    }
}
