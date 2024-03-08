using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewHotel.Models
{
    [Table("Pensioni")]
    public class Pensione
    {
        [Key]
        public int IdPensione { get; set; }

        [Required(ErrorMessage = "Il tipo di pensione è obbligatorio.")]
        public TipiPensione TipoPensione { get; set; }

        [Required(ErrorMessage = "Il costo giornaliero è obbligatorio.")]
        public int CostoGiornaliero { get; set; }

        public virtual ICollection<Prenotazione> Prenotazioni { get; set; }

    }
}
