using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rad.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPoolImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PoolImg",
                table: "Accomodations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "PoolDistance", "PoolImg" },
                values: new object[] { 200, "/images/braco_bazen.png" });

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "PoolDistance", "PoolImg" },
                values: new object[] { 150, "/images/draga_bazen.png" });

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 3,
                column: "PoolImg",
                value: "/images/braco_bazen.png");

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "PoolDistance", "PoolImg" },
                values: new object[] { 400, "/images/laura_bazen.png" });

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 5,
                column: "PoolImg",
                value: "/images/laura_bazen.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PoolImg",
                table: "Accomodations");

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 1,
                column: "PoolDistance",
                value: 300);

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 2,
                column: "PoolDistance",
                value: 200);

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 4,
                column: "PoolDistance",
                value: 200);
        }
    }
}
