using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResources_Infrastructure.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 911, DateTimeKind.Local).AddTicks(4323));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 911, DateTimeKind.Local).AddTicks(4325));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 911, DateTimeKind.Local).AddTicks(4326));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 911, DateTimeKind.Local).AddTicks(4328));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 911, DateTimeKind.Local).AddTicks(4329));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 911, DateTimeKind.Local).AddTicks(4330));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 911, DateTimeKind.Local).AddTicks(1784));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 911, DateTimeKind.Local).AddTicks(1787));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 911, DateTimeKind.Local).AddTicks(1788));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 911, DateTimeKind.Local).AddTicks(1789));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 911, DateTimeKind.Local).AddTicks(1790));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 911, DateTimeKind.Local).AddTicks(1791));

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 911, DateTimeKind.Local).AddTicks(4925));

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 911, DateTimeKind.Local).AddTicks(4927));

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 911, DateTimeKind.Local).AddTicks(4928));

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 911, DateTimeKind.Local).AddTicks(4929));

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 911, DateTimeKind.Local).AddTicks(4930));

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 911, DateTimeKind.Local).AddTicks(4931));

            migrationBuilder.UpdateData(
                table: "ExpenseTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 912, DateTimeKind.Local).AddTicks(1610));

            migrationBuilder.UpdateData(
                table: "ExpenseTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 912, DateTimeKind.Local).AddTicks(1613));

            migrationBuilder.UpdateData(
                table: "ExpenseTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 912, DateTimeKind.Local).AddTicks(1614));

            migrationBuilder.UpdateData(
                table: "ExpenseTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 912, DateTimeKind.Local).AddTicks(1615));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 910, DateTimeKind.Local).AddTicks(9983));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 910, DateTimeKind.Local).AddTicks(9994));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 20, 40, 910, DateTimeKind.Local).AddTicks(9995));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(6723));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(6729));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(6730));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(6731));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(6732));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(6733));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(3455));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(3457));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(3458));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(3460));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(3461));

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(3462));

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(7453));

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(7455));

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(7456));

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(7458));

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(7459));

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(7460));

            migrationBuilder.UpdateData(
                table: "ExpenseTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 360, DateTimeKind.Local).AddTicks(4957));

            migrationBuilder.UpdateData(
                table: "ExpenseTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 360, DateTimeKind.Local).AddTicks(4964));

            migrationBuilder.UpdateData(
                table: "ExpenseTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 360, DateTimeKind.Local).AddTicks(4965));

            migrationBuilder.UpdateData(
                table: "ExpenseTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 360, DateTimeKind.Local).AddTicks(4966));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(1150));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(1166));

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2023, 6, 3, 21, 9, 7, 359, DateTimeKind.Local).AddTicks(1167));
        }
    }
}
