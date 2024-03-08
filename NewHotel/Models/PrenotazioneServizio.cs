using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewHotel.Models
{
    [Table("PrenotazioniServizi")]
    public class PrenotazioneServizio
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Prenotazione")]
        public int IdPrenotazione { get; set; }

        [ForeignKey("Servizio")]
        public int IdServizio { get; set; }

        public virtual Prenotazione Prenotazione { get; set; }
        public virtual Servizio Servizio { get; set; }
    }
}
