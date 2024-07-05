using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsurTech.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMotorPlanRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MotorPlanRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PersonalAccident = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Theft = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThirdPartyLiability = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OwnDamage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LegalExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorPlanRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotorPlanRequests_Requests_Id",
                        column: x => x.Id,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0de9f5bc-f786-4f58-8d80-68412df97592", "AQAAAAIAAYagAAAAEBZJj/86BungJebSkUyz8TKlLta5l7yjntthsjqi1rjWwLZbFBPmxDPBEDXb1vdC3w==", "82cdf6bf-90f7-4e60-a489-7ffae924b93f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MotorPlanRequests");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9327b97e-313a-4464-870a-3aeae85e93e1", "AQAAAAIAAYagAAAAEPpADZqbUK676tm5MjcSypsMj5TvcUnG+mw/e6oWXZAv71M055/RoarAIzrt7BEp8Q==", "bfe9bd2b-7e30-482a-84f4-161c1ae50b33" });
        }
    }
}
