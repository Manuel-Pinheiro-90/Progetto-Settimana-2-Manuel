using Progetto_Settimana_2_Manuel.Models;

namespace Progetto_Settimana_2_Manuel.DAO
{
    public interface ICameraDAO
    {
        IEnumerable<Camera> GetAll();
        Camera GetById(int id);
        void Add(Camera camera);
        void Update(Camera camera);
        void Delete(int id);
    }

}
