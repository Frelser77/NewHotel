using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewHotel.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servizi_Prenotazioni_PrenotazioneIdPrenotazione",
                table: "Servizi");

            migrationBuilder.AlterColumn<int>(
                name: "PrenotazioneIdPrenotazione",
                table: "Servizi",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdPrenotazione",
                table: "Servizi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Servizi_Prenotazioni_PrenotazioneIdPrenotazione",
                table: "Servizi",
                column: "PrenotazioneIdPrenotazione",
                principalTable: "Prenotazioni",
                principalColumn: "IdPrenotazione");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servizi_Prenotazioni_PrenotazioneIdPrenotazione",
                table: "Servizi");

            migrationBuilder.DropColumn(
                name: "IdPrenotazione",
                table: "Servizi");

            migrationBuilder.AlterColumn<int>(
                name: "PrenotazioneIdPrenotazione",
                table: "Servizi",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Servizi_Prenotazioni_PrenotazioneIdPrenotazione",
                table: "Servizi",
                column: "PrenotazioneIdPrenotazione",
                principalTable: "Prenotazioni",
                principalColumn: "IdPrenotazione",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
