using Humanizer.Localisation;
using Progetto_Settimana_2_Manuel.Models;
namespace Progetto_Settimana_2_Manuel.Service
{
    public interface IAuthService
    {
        Utente Login(String username, String password);

        public Utente CreateUser(Utente user);
        public Utente Register(Utente user);

    }
}
