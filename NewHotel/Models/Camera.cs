using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewHotel.Models
{
    [Table("Camere")]
    public class Camera
    {
        [Key]
        public int IdCamera { get; set; }

        [Required]
        [Display(Name = "Numero Camera")]
        public int NumeroCamera { get; set; }

        [Required(ErrorMessage = "La descrizione è obbligatoria.")]
        [StringLength(255, ErrorMessage = "La descrizione non può superare i 255 caratteri.")]
        public string Descrizione { get; set; }

        [Required]
        [Display(Name = "Tipologia camera")]
        [StringLength(50, ErrorMessage = "Il tipo di camera non può superare i 50 caratteri.")]
        public string TipoCamera { get; set; }

        public virtual ICollection<Prenotazione> Prenotazioni { get; set; }

    }
}
