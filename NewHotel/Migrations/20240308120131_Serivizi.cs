using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewHotel.Migrations
{
    /// <inheritdoc />
    public partial class Serivizi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servizi_Prenotazioni_IdPrenotazione",
                table: "Servizi");

            migrationBuilder.DropIndex(
                name: "IX_Servizi_IdPrenotazione",
                table: "Servizi");

            migrationBuilder.AddColumn<int>(
                name: "PrenotazioneIdPrenotazione",
                table: "Servizi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PrenotazioniServizi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPrenotazione = table.Column<int>(type: "int", nullable: false),
                    IdServizio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrenotazioniServizi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrenotazioniServizi_Prenotazioni_IdPrenotazione",
                        column: x => x.IdPrenotazione,
                        principalTable: "Prenotazioni",
                        principalColumn: "IdPrenotazione",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrenotazioniServizi_Servizi_IdServizio",
                        column: x => x.IdServizio,
                        principalTable: "Servizi",
                        principalColumn: "IdServizio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servizi_PrenotazioneIdPrenotazione",
                table: "Servizi",
                column: "PrenotazioneIdPrenotazione");

            migrationBuilder.CreateIndex(
                name: "IX_PrenotazioniServizi_IdPrenotazione",
                table: "PrenotazioniServizi",
                column: "IdPrenotazione");

            migrationBuilder.CreateIndex(
                name: "IX_PrenotazioniServizi_IdServizio",
                table: "PrenotazioniServizi",
                column: "IdServizio");

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

            migrationBuilder.DropTable(
                name: "PrenotazioniServizi");

            migrationBuilder.DropIndex(
                name: "IX_Servizi_PrenotazioneIdPrenotazione",
                table: "Servizi");

            migrationBuilder.DropColumn(
                name: "PrenotazioneIdPrenotazione",
                table: "Servizi");

            migrationBuilder.CreateIndex(
                name: "IX_Servizi_IdPrenotazione",
                table: "Servizi",
                column: "IdPrenotazione");

            migrationBuilder.AddForeignKey(
                name: "FK_Servizi_Prenotazioni_IdPrenotazione",
                table: "Servizi",
                column: "IdPrenotazione",
                principalTable: "Prenotazioni",
                principalColumn: "IdPrenotazione",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
