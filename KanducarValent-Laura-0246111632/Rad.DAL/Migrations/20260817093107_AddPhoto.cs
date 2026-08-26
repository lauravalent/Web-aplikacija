using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rad.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccomodationID = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Photo_Accomodations_AccomodationID",
                        column: x => x.AccomodationID,
                        principalTable: "Accomodations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Description", "Name", "PoolDistance", "PricePerNight", "Size" },
                values: new object[] { "Kuća Marijan je prostor koji može primiti dvoje ljudi, a nalazi se nedaleko od maslenika i voćnjaka pa je najbolje vrijeme za posjet ljeto i rana jesen.Unutrašnjost je inspirirana kućama iz prošlosti koje su prije nekoliko godina na našim prostorima bile bijeg od gradske vreve.Osim krova nad glavom, ispred same kuće postoji terasa na kojima se provode tople ljetne večeri uživajući u tišini i ugodnim temperaturama.", "Kuća Marijan", 300, 50.00m, 30 });

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Capacity", "Name", "PricePerNight", "Size" },
                values: new object[] { 5, "Kuća Draga", 90.00m, 50 });

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "Capacity", "ImageUrl", "PricePerNight" },
                values: new object[] { 2, "/images/braco1.jpg", 70.00m });

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "Capacity", "ImageUrl", "Name" },
                values: new object[] { 4, "/images/laura.jpg", "Kuća Laura" });

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 5,
                column: "Name",
                value: "Kuća Janko");

            migrationBuilder.InsertData(
                table: "Photo",
                columns: new[] { "ID", "AccomodationID", "ImageUrl" },
                values: new object[,]
                {
                    { 1, 1, "/images/marijan1.jpeg" },
                    { 2, 1, "/images/marijan2.jpeg" },
                    { 3, 1, "/images/marijan3.jpeg" },
                    { 4, 2, "/images/draga1.jpeg" },
                    { 5, 2, "/images/draga2.jpeg" },
                    { 6, 2, "/images/draga3.jpeg" },
                    { 7, 2, "/images/draga4.jpeg" },
                    { 8, 3, "/images/braco1.jpeg" },
                    { 9, 3, "/images/braco2.jpeg" },
                    { 10, 3, "/images/braco3.jpeg" },
                    { 11, 4, "/images/laura1.jpeg" },
                    { 12, 4, "/images/laura2.jpeg" },
                    { 13, 4, "/images/laura3.jpeg" },
                    { 14, 4, "/images/laura4.jpeg" },
                    { 15, 4, "/images/laura5.jpeg" },
                    { 16, 4, "/images/laura6.jpeg" },
                    { 17, 5, "/images/janko1.jpeg" },
                    { 18, 5, "/images/janko2.jpeg" },
                    { 19, 5, "/images/janko3.jpeg" },
                    { 20, 5, "/images/janko4.jpeg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photo_AccomodationID",
                table: "Photo",
                column: "AccomodationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Description", "Name", "PoolDistance", "PricePerNight", "Size" },
                values: new object[] { "Apartman Sofija je moderan i udoban apartman smješten u srcu grada. Sadrži sve potrebne sadržaje za ugodan boravak, uključujući potpuno opremljenu kuhinju, prostranu dnevnu sobu i udobnu spavaću sobu. Idealno za parove ili male obitelji.", "Apartman Sofija", 200, 75.00m, 45 });

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Capacity", "Name", "PricePerNight", "Size" },
                values: new object[] { 3, "Apartman Draga", 65.00m, 35 });

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "Capacity", "ImageUrl", "PricePerNight" },
                values: new object[] { 4, "/images/draga.jpg", 85.00m });

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "Capacity", "ImageUrl", "Name" },
                values: new object[] { 2, "/images/ivica.jpg", "Kuca Ivica" });

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 5,
                column: "Name",
                value: "Apartman More");

            migrationBuilder.InsertData(
                table: "Accomodations",
                columns: new[] { "ID", "Capacity", "Description", "ImageUrl", "Name", "PoolDistance", "PricePerNight", "Size" },
                values: new object[] { 6, 6, "Apartman Sofija je moderan i udoban apartman smješten u srcu grada. Sadrži sve potrebne sadržaje za ugodan boravak, uključujući potpuno opremljenu kuhinju, prostranu dnevnu sobu i udobnu spavaću sobu. Idealno za parove ili male obitelji.", "/images/planina.jpeg", "Vila Planina", 200, 150.00m, 120 });
        }
    }
}
