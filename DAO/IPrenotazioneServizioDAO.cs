using Progetto_Settimana_2_Manuel.Models;

namespace Progetto_Settimana_2_Manuel.DAO
{
    public interface IPrenotazioneServizioDAO
    {
        IEnumerable<PrenotazioneServizio> GetAll();
        PrenotazioneServizio GetById(int id);
        void Add(PrenotazioneServizio prenotazioneServizio);
        void Update(PrenotazioneServizio prenotazioneServizio);
        void Delete(int id);





    }
}
