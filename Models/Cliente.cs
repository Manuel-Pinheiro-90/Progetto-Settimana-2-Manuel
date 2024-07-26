using System.ComponentModel.DataAnnotations;

namespace Progetto_Settimana_2_Manuel.Models
{
    public class Cliente
    {
        public int ID { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 16)]
        public string CodiceFiscale { get; set; }
        [Required]
        [StringLength(50)]
        public string Cognome { get; set; }
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        [StringLength(100)]
        public string Citta {  get; set; }
        [Required]
        [StringLength(100)]
        public string Provincia { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Telefono { get; set; }
        [Phone]
        public string Cellulare { get; set; }


    }
}
