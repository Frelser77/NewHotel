using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewHotel.Models
{

    [Table("Clienti")]
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il cognome non può superare i 100 caratteri.")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il nome non può superare i 100 caratteri.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il codice fiscale è obbligatorio.")]
        [StringLength(16, ErrorMessage = "Il codice fiscale deve essere di 16 caratteri.")]
        [Display(Name = "Codice Fiscale")]
        public string CF { get; set; }

        [StringLength(100, ErrorMessage = "La città non può superare i 100 caratteri.")]
        [Display(Name = "Città")]
        public string Citta { get; set; }

        [StringLength(100, ErrorMessage = "La provincia non può superare i 100 caratteri.")]
        public string Provincia { get; set; }

        [EmailAddress(ErrorMessage = "Formato email non valido.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Formato numero di telefono non valido.")]
        public string Telefono { get; set; }

        [Phone(ErrorMessage = "Formato numero di cellulare non valido.")]
        public string Cellulare { get; set; }


        public virtual ICollection<Prenotazione> Prenotazioni { get; set; }

    }
}

