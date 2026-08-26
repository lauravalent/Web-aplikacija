using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rad.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccomodationAndReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accomodations_Cities_CityID",
                table: "Accomodations");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Accomodations_CityID",
                table: "Accomodations");

            migrationBuilder.RenameColumn(
                name: "CityID",
                table: "Accomodations",
                newName: "PoolDistance");

            migrationBuilder.AddColumn<string>(
                name: "GuestEmail",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuestName",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuestPhone",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerNight",
                table: "Accomodations",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Accomodations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Description", "PoolDistance" },
                values: new object[] { "Apartman Sofija je moderan i udoban apartman smješten u srcu grada. Sadrži sve potrebne sadržaje za ugodan boravak, uključujući potpuno opremljenu kuhinju, prostranu dnevnu sobu i udobnu spavaću sobu. Idealno za parove ili male obitelji.", 200 });

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Description", "PoolDistance" },
                values: new object[] { "Apartman Sofija je moderan i udoban apartman smješten u srcu grada. Sadrži sve potrebne sadržaje za ugodan boravak, uključujući potpuno opremljenu kuhinju, prostranu dnevnu sobu i udobnu spavaću sobu. Idealno za parove ili male obitelji.", 200 });

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "Description", "PoolDistance" },
                values: new object[] { "Apartman Sofija je moderan i udoban apartman smješten u srcu grada. Sadrži sve potrebne sadržaje za ugodan boravak, uključujući potpuno opremljenu kuhinju, prostranu dnevnu sobu i udobnu spavaću sobu. Idealno za parove ili male obitelji.", 200 });

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "Description", "PoolDistance" },
                values: new object[] { "Apartman Sofija je moderan i udoban apartman smješten u srcu grada. Sadrži sve potrebne sadržaje za ugodan boravak, uključujući potpuno opremljenu kuhinju, prostranu dnevnu sobu i udobnu spavaću sobu. Idealno za parove ili male obitelji.", 200 });

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "Description", "PoolDistance" },
                values: new object[] { "Apartman Sofija je moderan i udoban apartman smješten u srcu grada. Sadrži sve potrebne sadržaje za ugodan boravak, uključujući potpuno opremljenu kuhinju, prostranu dnevnu sobu i udobnu spavaću sobu. Idealno za parove ili male obitelji.", 200 });

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "Description", "PoolDistance" },
                values: new object[] { "Apartman Sofija je moderan i udoban apartman smješten u srcu grada. Sadrži sve potrebne sadržaje za ugodan boravak, uključujući potpuno opremljenu kuhinju, prostranu dnevnu sobu i udobnu spavaću sobu. Idealno za parove ili male obitelji.", 200 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuestEmail",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "GuestName",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "GuestPhone",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Accomodations");

            migrationBuilder.RenameColumn(
                name: "PoolDistance",
                table: "Accomodations",
                newName: "CityID");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerNight",
                table: "Accomodations",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                });

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 1,
                column: "CityID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 2,
                column: "CityID",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 3,
                column: "CityID",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 4,
                column: "CityID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 5,
                column: "CityID",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 6,
                column: "CityID",
                value: 3);

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Sirova Katalena" },
                    { 2, "Budrovac" },
                    { 3, "Sveta Ana" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accomodations_CityID",
                table: "Accomodations",
                column: "CityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accomodations_Cities_CityID",
                table: "Accomodations",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "ID");
        }
    }
}
