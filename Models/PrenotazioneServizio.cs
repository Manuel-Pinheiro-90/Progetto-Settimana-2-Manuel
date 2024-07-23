namespace Progetto_Settimana_2_Manuel.Models
{
    public class PrenotazioneServizio
    {
        public int ID { get; set; }
        public int NumeroPrenotazione { get; set; }
        public int ServizioID { get; set; }
        public DateTime Data { get; set; }
        public int Quantita { get; set; }
    }
}
