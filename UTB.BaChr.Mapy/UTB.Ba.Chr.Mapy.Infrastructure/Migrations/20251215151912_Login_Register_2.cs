using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UTB.BaChr.Mapy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Login_Register_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "Description", "MapX", "MapY", "Name" },
                values: new object[,]
                {
                    { 1, "Henryho strážní věž. Domov daleko od domova.", 1175.0, 1240.0, "Two Forks Tower" },
                    { 2, "Thorofare Lookout. Věž, kde slouží Delilah.", 1650.0, 1600.0, "Delilah's Tower" },
                    { 3, "Klidné jezero ideální pro rybaření a přemýšlení.", 1350.0, 950.0, "Jonesy Lake" },
                    { 4, "Místo s nádherným výhledem na celé údolí.", 900.0, 1450.0, "Beartooth Point" },
                    { 5, "Oplocená výzkumná stanice. Vstup zakázán.", 700.0, 1100.0, "Wapiti Station" },
                    { 6, "Potok protékající jižní částí lesa.", 1400.0, 600.0, "Cottonwood Creek" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
