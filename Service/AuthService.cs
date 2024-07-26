using Progetto_Settimana_2_Manuel.Models;
using System.Data.SqlClient;

namespace Progetto_Settimana_2_Manuel.Service
{
    public class AuthService : IAuthService

    {
        private string connectionString;
        private const string LOGIN_COMMAND = "SELECT IdUser, Username, PasswordHash FROM dbo.Users WHERE Username = @username AND PasswordHash = @password";
        private const string ROLES_COMMAND = "SELECT Nome FROM Ruoli ro JOIN UsersRuoli ur ON ro.IdRuolo = ur.IdRuolo WHERE IdUser =@id";


        public AuthService(IPasswordEncoder passwordEncoder, IConfiguration config) : base()
        {
            _passwordEncoder = passwordEncoder;
            connectionString = config.GetConnectionString("CON")!;
        }





        public Utente Login(string username, string password)
        {
            try
            {
                var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(LOGIN_COMMAND, conn);
                cmd.Parameters.AddWithValue("@username", username);
                var EncodedPass = _passwordEncoder.Encode(password);
                cmd.Parameters.AddWithValue("@password", EncodedPass);
                using var reader = cmd.ExecuteReader();
                
                if (reader.Read())
                {
                    var utente = new Utente
                    {
                        IdUtente = reader.GetInt32(0),
                        Username = username,
                        PasswordHash = password,

                    };
                    reader.Close();
                    using var roleCmd = new SqlCommand(ROLES_COMMAND, conn);
                    roleCmd.Parameters.AddWithValue("@id", utente.IdUtente);
                    using var re = roleCmd.ExecuteReader();
                    while (re.Read()) { utente.Ruolo.Add(re.GetString(0)); }
                    return utente;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Errore durante il login.", ex);
            }
            return null;



        }




        private const string INSERT_USER = "INSERT INTO Users(Username, PasswordHash) OUTPUT INSERTED.IdUser VALUES(@username, @passwordHash)";
        private readonly IPasswordEncoder _passwordEncoder;
      

        // //////////////Non ho aggiunto la possibilità di dare ruoli agli utenti deve essere dato tramite data base.

        public Utente CreateUser(Utente user)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(INSERT_USER, conn);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@passwordHash", user.PasswordHash);
                user.IdUtente = (int)cmd.ExecuteScalar();
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Utente Register(Utente user)
        {
            var u = CreateUser(
            new Utente
            {
                PasswordHash = _passwordEncoder.Encode(user.PasswordHash),
                Username = user.Username
            });
            return new Utente { IdUtente = u.IdUtente, PasswordHash = u.PasswordHash, Username = u.Username };
        }









    }
}
