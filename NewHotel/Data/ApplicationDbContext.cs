using Microsoft.EntityFrameworkCore;
using NewHotel.Models;

namespace NewHotel.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Utente> Utente { get; set; }
        public DbSet<Cliente> Clienti { get; set; }
        public DbSet<Camera> Camere { get; set; }
        public DbSet<Pensione> Pensioni { get; set; }
        public DbSet<Prenotazione> Prenotazioni { get; set; }
        public DbSet<Servizio> Servizi { get; set; }

        public DbSet<PrenotazioneServizio> PrenotazioniServizi { get; set; }
    }
}
