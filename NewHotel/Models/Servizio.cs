using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewHotel.Models
{
    public class Servizio
    {
        [Key]
        public int IdServizio { get; set; }

        [Required(ErrorMessage = "Il tipo di servizio è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il tipo di servizio non può superare i 100 caratteri.")]
        public string TipoServizio { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "La quantità deve essere un numero positivo.")]
        [Display(Name = "Quantità")]
        public int Quantita { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Prezzo Totale")]
        public decimal PrezzoTot { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data Aggiunta")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataAggiunta { get; set; } = DateTime.Today;

        // Chiave esterna verso la tabella Prenotazioni
        [ForeignKey("Prenotazione")]
        public int IdPrenotazione { get; set; }

        public virtual ICollection<PrenotazioneServizio> PrenotazioneServizi { get; set; }
    }
}
