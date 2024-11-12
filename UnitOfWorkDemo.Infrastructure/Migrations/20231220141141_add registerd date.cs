using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.Infrastructure.Migrations
{
    public partial class addregisterddate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredDate",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GetEmployeeStatisticsDto",
                columns: table => new
                {
                    TotalEmployees = table.Column<int>(type: "int", nullable: false),
                    NewEmployeesToday = table.Column<int>(type: "int", nullable: false),
                    NewEmployeesThisWeek = table.Column<int>(type: "int", nullable: false),
                    ActiveEmployees = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GetEmployeeStatisticsDto");

            migrationBuilder.DropColumn(
                name: "RegisteredDate",
                table: "Employees");
        }
    }
}
