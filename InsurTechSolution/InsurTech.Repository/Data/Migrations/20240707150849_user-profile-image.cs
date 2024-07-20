using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsurTech.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class userprofileimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "Image", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d5a70ac-0bda-4b9a-b511-d529a92dac52", null, "AQAAAAIAAYagAAAAEIVS4LkswPwq8+aOBZKRPRkJdmdtOqXVSxPQwiAieOIlFyHF4HG2OQWeqAYTI05HWA==", "c6aaa0a8-8848-42ca-a816-2d95f089c50e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0de9f5bc-f786-4f58-8d80-68412df97592", "AQAAAAIAAYagAAAAEBZJj/86BungJebSkUyz8TKlLta5l7yjntthsjqi1rjWwLZbFBPmxDPBEDXb1vdC3w==", "82cdf6bf-90f7-4e60-a489-7ffae924b93f" });
        }
    }
}
