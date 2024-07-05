using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsurTech.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHealthPlanRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthPlanRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MedicalNetwork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClinicsCoverage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HospitalizationAndSurgery = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpticalCoverage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DentalCoverage = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthPlanRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthPlanRequests_Requests_Id",
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
                values: new object[] { "c55b530b-c146-4a19-bbf4-971d4bb69da6", "AQAAAAIAAYagAAAAEJ3tLsrw4LbvEAJwJB5ntm+G1qyl7kGiL0YlmrGoMiDEaVjn/bIxrPCMG+v+BEQCpA==", "5efe9d96-2580-425e-bdb4-d37d9fa99e42" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthPlanRequests");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7113e784-9648-4ada-a045-bb56d52b18f3", "AQAAAAIAAYagAAAAEFRTFxzwj1eGcFmKc50fZmt2KASd+3Iboupk7CwH61sUkQTRaKZ7lYb4VsA5USicEQ==", "b8ae6458-1a29-418b-a5be-2c62b6c93704" });
        }
    }
}
