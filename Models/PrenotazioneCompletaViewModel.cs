namespace Progetto_Settimana_2_Manuel.Models
{
    public class PrenotazioneCompletaViewModel
    {
        public int PrenotazioneId { get; set; }
        public Cliente Cliente { get; set; }
        public Camera Camera { get; set; }
        public DateTime DataPrenotazione { get; set; }
        public DateTime PeriodoDal { get; set; }
        public DateTime PeriodoAl { get; set; }
        public decimal CaparraConfirmatoria { get; set; }
        public decimal TariffaApplicata { get; set; }
        public string TipoSoggiorno { get; set; }
        public List<ServizioRiepilogoViewModel> ServiziAggiuntivi { get; set; }


    }
}
