using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTB.BaChr.Mapy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LocationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MapX", "MapY" },
                values: new object[] { 1268.0, 1395.0 });

            migrationBuilder.UpdateData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MapX", "MapY" },
                values: new object[] { 1132.0, 225.0 });

            migrationBuilder.UpdateData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "MapX", "MapY" },
                values: new object[] { 275.0, 1206.0 });

            migrationBuilder.UpdateData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "MapX", "MapY" },
                values: new object[] { 1283.0, 323.0 });

            migrationBuilder.UpdateData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "MapX", "MapY" },
                values: new object[] { 562.0, 770.0 });

            migrationBuilder.UpdateData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "MapX", "MapY" },
                values: new object[] { 1013.0, 1787.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MapX", "MapY" },
                values: new object[] { 1175.0, 1240.0 });

            migrationBuilder.UpdateData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MapX", "MapY" },
                values: new object[] { 1650.0, 1600.0 });

            migrationBuilder.UpdateData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "MapX", "MapY" },
                values: new object[] { 1350.0, 950.0 });

            migrationBuilder.UpdateData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "MapX", "MapY" },
                values: new object[] { 900.0, 1450.0 });

            migrationBuilder.UpdateData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "MapX", "MapY" },
                values: new object[] { 700.0, 1100.0 });

            migrationBuilder.UpdateData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "MapX", "MapY" },
                values: new object[] { 1400.0, 600.0 });
        }
    }
}
