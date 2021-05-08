using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VSTForum.Migrations
{
    public partial class SeedTestPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Body", "CategoryId", "DateCreated", "Title", "UserId" },
                values: new object[] { "0", "This is a test post.", "0", new DateTime(2021, 1, 1, 8, 30, 0, 0, DateTimeKind.Unspecified), "Test Post", "cc0edea5-1a9c-4e1f-aa00-61fbdcfc00bd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: "0");
        }
    }
}
