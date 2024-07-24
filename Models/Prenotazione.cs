namespace Progetto_Settimana_2_Manuel.Models
{
    public class Prenotazione
    {
        public int ID { get; set; }
        public int CodiceFiscaleCliente { get; set; }
       
        public int NumeroCamera { get; set; }
        public DateTime DataPrenotazione { get; set; }
        public int Anno { get; set; }
        public DateTime PeriodoDal  { get; set; }
        public DateTime PeriodoAl { get; set; }
        public decimal? CaparraConfirmatoria { get; set; }
        public decimal TariffaApplicata { get; set; }
        public string TipoSoggiorno { get; set; }






    }
}
