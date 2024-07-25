

namespace Progetto_Settimana_2_Manuel.Models
{
    public class PrenotazioneViewModel
    {
        public int ClienteId { get; set; }
        public int CameraId { get; set; }
        public DateTime DataPrenotazione { get; set; }
        public DateTime PeriodoDal { get; set; }
        public DateTime PeriodoAl { get; set; }
        public decimal CaparraConfirmatoria { get; set; }
        public decimal TariffaApplicata { get; set; }
        public string TipoSoggiorno { get; set; }

        public IEnumerable<Cliente> Clienti { get; set; }
        public IEnumerable<Camera> Camere { get; set; }
        public IEnumerable<Servizio> Servizi { get; set; }
        public List<int> ServiziSelezionati { get; set; } = new List<int>();
        public Dictionary<int, int> QuantitaServizi { get; set; } = new Dictionary<int, int>();



    }
}
