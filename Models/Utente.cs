namespace Progetto_Settimana_2_Manuel.Models
{
    public class Utente
    {
        public int IdUtente { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public List<string> Ruolo { get; set; } = [];
    }
}
