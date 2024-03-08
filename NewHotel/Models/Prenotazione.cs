using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewHotel.Models
{
    [Table("Prenotazioni")]
    public class Prenotazione
    {
        [Key]
        public int IdPrenotazione { get; set; }

        [Required]
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        [Required]
        [ForeignKey("Camera")]
        public int IdCamera { get; set; }

        [Required]
        [ForeignKey("Pensione")]
        public int IdPensione { get; set; }

        [Required]
        [Display(Name = "Data Prenotazione")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DataPrenotazione { get; set; }

        [Required]
        [Display(Name = "Data Check-In")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DataCheckIn { get; set; }

        [Required]
        [Display(Name = "Data Check-Out")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DataCheckOut { get; set; }

        [Display(Name = "Numero di Ospiti")]
        public int NumeroOspiti { get; set; }

        [Display(Name = "Acconto")]
        public int Acconto { get; set; }

        [Display(Name = "Prezzo Totale")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrezzoTotale { get; set; }

        [StringLength(500)]
        [Display(Name = "Note Prenotazione")]
        public string Note { get; set; }

        // Proprietà di navigazione verso Cliente
        public virtual Cliente Cliente { get; set; }

        // Proprietà di navigazione verso Camera
        public virtual Camera Camera { get; set; }

        public virtual Pensione Pensione { get; set; }

        public virtual ICollection<Servizio> Servizi { get; set; }

        public virtual ICollection<PrenotazioneServizio> PrenotazioneServizi { get; set; }
    }
}
