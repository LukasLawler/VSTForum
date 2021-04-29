using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VSTForum.Migrations
{
    public partial class SeedTestReply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PostReplies",
                columns: new[] { "ReplyId", "Body", "PostId", "DateCreated", "UserId" },
                values: new object[] { "0", "This is a test reply", "0", new DateTime(2021, 1, 1, 8, 30, 0, 0, DateTimeKind.Unspecified), "879cd390-6ba6-4f80-9837-1407f622c20b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PostReplies",
                keyColumn: "ReplyId",
                keyValue: "0");
        }
    }
}
