using Progetto_Settimana_2_Manuel.Models;
using Progetto_Settimana_2_Manuel.Service;
using System.Data.SqlClient;

namespace Progetto_Settimana_2_Manuel.DAO
{
    public class ServizioDAO : IServizioDAO
    {
        private readonly SqlServerServiceBase _dbService;

        public ServizioDAO(SqlServerServiceBase dbService)
        {
            _dbService = dbService;
        }

        private const string GET_ALL_SERVIZI = "SELECT * FROM Servizi";
        private const string GET_SERVIZIO_BY_ID = "SELECT * FROM Servizi WHERE ID = @ID";
        private const string CREATE_SERVIZIO = "INSERT INTO Servizi (Descrizione, Prezzo) " +
                                               "OUTPUT INSERTED.ID " +
                                               "VALUES (@Descrizione, @Prezzo)";
        private const string UPDATE_SERVIZIO = "UPDATE Servizi SET Descrizione = @Descrizione, Prezzo = @Prezzo WHERE ID = @ID";
        private const string DELETE_SERVIZIO = "DELETE FROM Servizi WHERE ID = @ID";



        // /////////////////////////////////////////// GET ALL ///////////////////////////////////////////////////////
        public IEnumerable<Servizio> GetAll()
        {
            var result = new List<Servizio>();

           var conn = _dbService.GetConnection();
            {
                conn.Open();
                using var command = _dbService.GetCommand(conn, GET_ALL_SERVIZI);
                using var reader = command.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        result.Add(new Servizio
                        {
                            ID = reader.GetInt32(0),
                            Descrizione = reader.GetString(1),
                            Prezzo = reader.GetDecimal(2)
                        });
                    }
                }
                conn.Close();
            }

            return result;
        }



        // /////////////////////////////////////////// GET BY ID ///////////////////////////////////////////////////////
        public Servizio GetById(int id)
        {
            Servizio servizio = null;

             var conn = _dbService.GetConnection();
            {
                conn.Open();
                using var command = _dbService.GetCommand(conn, GET_SERVIZIO_BY_ID);
                
                    command.Parameters.Add(new SqlParameter("@ID", id));

                using var reader = command.ExecuteReader();
                    
                        if (reader.Read())
                        {
                            servizio = new Servizio
                            {
                                ID = reader.GetInt32(0),
                                Descrizione = reader.GetString(1),
                                Prezzo = reader.GetDecimal(2)
                            };
                       }
                    
                
                conn.Close();
            }

            return servizio;
        }

        // /////////////////////////////////////////// ADD ///////////////////////////////////////////////////////
        public void Add(Servizio servizio)
        {
           var conn = _dbService.GetConnection();
            
                conn.Open();
                using var transaction = conn.BeginTransaction();
                {
                try
                {
                    using var command = _dbService.GetCommand(conn, CREATE_SERVIZIO);

                    command.Transaction = transaction;
                    command.Parameters.Add(new SqlParameter("@Descrizione", servizio.Descrizione));
                    command.Parameters.Add(new SqlParameter("@Prezzo", servizio.Prezzo));

                    servizio.ID = (int)command.ExecuteScalar();
                    transaction.Commit();

                }
                catch (Exception)
                {
                    transaction.Rollback();

                    throw;
                }
                finally { conn.Close(); }

                }
            
        }
        // /////////////////////////////////////////// UPDATE ///////////////////////////////////////////////////////
        public void Update(Servizio servizio)
        {
             var conn = _dbService.GetConnection();
            
                conn.Open();
                using var transaction = conn.BeginTransaction();
                {
                    try
                    {
                        using var command = _dbService.GetCommand(conn, UPDATE_SERVIZIO);
                        
                            command.Transaction = transaction;
                            command.Parameters.Add(new SqlParameter("@ID", servizio.ID));
                            command.Parameters.Add(new SqlParameter("@Descrizione", servizio.Descrizione));
                            command.Parameters.Add(new SqlParameter("@Prezzo", servizio.Prezzo));

                            command.ExecuteNonQuery();
                            transaction.Commit();
                        
                    }
                    catch (Exception )
                    {
                        transaction.Rollback();
                      
                        throw;
                    }
                  
                }
            
        }
        // /////////////////////////////////////////// DELETE ///////////////////////////////////////////////////////
        public void Delete(int id)
        {
            var conn = _dbService.GetConnection();
            {
                conn.Open();
                using var transaction = conn.BeginTransaction();
                
                    try
                    {
                    using var command = _dbService.GetCommand(conn, DELETE_SERVIZIO);
                        
                            command.Transaction = transaction;
                            command.Parameters.Add(new SqlParameter("@ID", id));
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        
                    }
                    catch (Exception )
                    {
                        transaction.Rollback();
                     
                        throw;
                    }
                   
                
            }
        }










    }
}
