using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsurTech.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class paid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Requests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6eebeb0-08a3-4f44-85df-433897317cba", "AQAAAAIAAYagAAAAEPa6AOAFTZMVN02zHs6qKHrII8ehqoEm4UAkKc3dvZI62zrIWBQNJJ4kXhCtWHS4Jg==", "13e9f41f-4afa-48e0-9dfd-143a98f275ce" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Requests");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0de9f5bc-f786-4f58-8d80-68412df97592", "AQAAAAIAAYagAAAAEBZJj/86BungJebSkUyz8TKlLta5l7yjntthsjqi1rjWwLZbFBPmxDPBEDXb1vdC3w==", "82cdf6bf-90f7-4e60-a489-7ffae924b93f" });
        }
    }
}
