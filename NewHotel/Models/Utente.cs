using System.ComponentModel.DataAnnotations;

namespace NewHotel.Models
{
    public class Utente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(18, ErrorMessage = "L'Username non può superare i 18 caratteri.")]
        public string Username { get; set; }

        [Required]
        [StringLength(18, ErrorMessage = "La Password non può superare i 18 caratteri.")]
        public string Password { get; set; }
    }
}
