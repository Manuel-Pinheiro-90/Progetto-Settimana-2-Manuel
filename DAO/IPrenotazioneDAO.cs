using Progetto_Settimana_2_Manuel.Models;

namespace Progetto_Settimana_2_Manuel.DAO
{
    public interface IPrenotazioneDAO
    {
        IEnumerable<Prenotazione> GetAll();
        Prenotazione GetById(int id);
        void Add(Prenotazione prenotazione);
        void Update(Prenotazione prenotazione);
        void Delete(int id);


    }
}
