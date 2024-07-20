using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsurTech.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHomePlanRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomePlanRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    WaterDamage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GlassBreakage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NaturalHazard = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AttemptedTheft = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FiresAndExplosion = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePlanRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomePlanRequests_Requests_Id",
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
                values: new object[] { "9327b97e-313a-4464-870a-3aeae85e93e1", "AQAAAAIAAYagAAAAEPpADZqbUK676tm5MjcSypsMj5TvcUnG+mw/e6oWXZAv71M055/RoarAIzrt7BEp8Q==", "bfe9bd2b-7e30-482a-84f4-161c1ae50b33" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomePlanRequests");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c55b530b-c146-4a19-bbf4-971d4bb69da6", "AQAAAAIAAYagAAAAEJ3tLsrw4LbvEAJwJB5ntm+G1qyl7kGiL0YlmrGoMiDEaVjn/bIxrPCMG+v+BEQCpA==", "5efe9d96-2580-425e-bdb4-d37d9fa99e42" });
        }
    }
}
