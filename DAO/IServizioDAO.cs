using Progetto_Settimana_2_Manuel.Models;

namespace Progetto_Settimana_2_Manuel.DAO
{
    public interface IServizioDAO
    {
        IEnumerable<Servizio> GetAll();
        Servizio GetById(int id);
        void Add(Servizio servizio);
        void Update(Servizio servizio);
        void Delete(int id);
    }
}
