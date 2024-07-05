using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsurTech.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class differentRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsurancePlans_AspNetUsers_CompanyId",
                table: "InsurancePlans");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Requests",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Quotation",
                table: "Requests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "YearlyCoverage",
                table: "Requests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Feedbacks",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7113e784-9648-4ada-a045-bb56d52b18f3", "AQAAAAIAAYagAAAAEFRTFxzwj1eGcFmKc50fZmt2KASd+3Iboupk7CwH61sUkQTRaKZ7lYb4VsA5USicEQ==", "b8ae6458-1a29-418b-a5be-2c62b6c93704" });

            migrationBuilder.AddForeignKey(
                name: "FK_InsurancePlans_AspNetUsers_CompanyId",
                table: "InsurancePlans",
                column: "CompanyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsurancePlans_AspNetUsers_CompanyId",
                table: "InsurancePlans");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Quotation",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "YearlyCoverage",
                table: "Requests");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "12eba7e0-33ce-4101-9c75-983a9221af15", "AQAAAAIAAYagAAAAEDO92CCHD+yaewcECcfixeZ0BE9UvgAXqCxKtlNPURfp4/3jskKCbcKpm1KBxNmWaQ==", "91c74ba0-27da-407f-a39f-3f0a4df48132" });

            migrationBuilder.AddForeignKey(
                name: "FK_InsurancePlans_AspNetUsers_CompanyId",
                table: "InsurancePlans",
                column: "CompanyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
