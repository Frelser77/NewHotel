using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewHotel.Migrations
{
    /// <inheritdoc />
    public partial class Acconto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Acconto",
                table: "Prenotazioni",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "TipoCamera",
                table: "Camere",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Acconto",
                table: "Prenotazioni");

            migrationBuilder.AlterColumn<int>(
                name: "TipoCamera",
                table: "Camere",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
