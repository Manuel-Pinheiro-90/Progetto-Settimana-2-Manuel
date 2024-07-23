using Progetto_Settimana_2_Manuel.Models;

namespace Progetto_Settimana_2_Manuel.DAO
{
    public interface IClienteDAO
    {
        IEnumerable<Cliente> GetAll();
        Cliente GetById(int id);
      
         void Add(Cliente cliente);
        void Update(Cliente cliente);
       void Delete(int id);

    }
}
