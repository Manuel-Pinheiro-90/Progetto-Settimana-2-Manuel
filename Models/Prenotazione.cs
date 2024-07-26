using System.ComponentModel.DataAnnotations;

namespace Progetto_Settimana_2_Manuel.Models
{
    public class Prenotazione
    {
       
        public int ID { get; set; }
        [Required]
        public int CodiceFiscaleCliente { get; set; }
        [Required]
        public int NumeroCamera { get; set; }
        [Required]
        public DateTime DataPrenotazione { get; set; }
        [Required]
        public int Anno { get; set; }
        [Required]
        public DateTime PeriodoDal  { get; set; }
        [Required]
        public DateTime PeriodoAl { get; set; }
        [Required]
        public decimal? CaparraConfirmatoria { get; set; }
        [Required]
        public decimal TariffaApplicata { get; set; }
        [Required]
        public string TipoSoggiorno { get; set; }






    }
}
