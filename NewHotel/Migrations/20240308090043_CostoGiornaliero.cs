using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewHotel.Migrations
{
    /// <inheritdoc />
    public partial class CostoGiornaliero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CostoGiornaliero",
                table: "Pensioni",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostoGiornaliero",
                table: "Pensioni");
        }
    }
}
