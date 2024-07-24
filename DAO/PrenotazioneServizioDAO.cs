using Progetto_Settimana_2_Manuel.Models;
using Progetto_Settimana_2_Manuel.Service;
using System.Data.SqlClient;

namespace Progetto_Settimana_2_Manuel.DAO
{
    public class PrenotazioneServizioDAO : IPrenotazioneServizioDAO
    {
        private readonly SqlServerServiceBase _dbService;

        public PrenotazioneServizioDAO(SqlServerServiceBase dbService)
        {
            _dbService = dbService;
        }

        private const string GET_ALL_PRENOTAZIONI_SERVIZI = "SELECT * FROM PrenotazioniServizi";
        private const string GET_PRENOTAZIONE_SERVIZIO_BY_ID = "SELECT * FROM PrenotazioniServizi WHERE ID = @ID";
        private const string CREATE_PRENOTAZIONE_SERVIZIO = "INSERT INTO PrenotazioniServizi (NumeroPrenotazione, ServizioID, Data, Quantita) " +
                                                            "OUTPUT INSERTED.ID " +
                                                            "VALUES (@NumeroPrenotazione, @ServizioID, @Data, @Quantita)";
        private const string UPDATE_PRENOTAZIONE_SERVIZIO = "UPDATE PrenotazioniServizi SET NumeroPrenotazione = @NumeroPrenotazione, ServizioID = @ServizioID, Data = @Data, Quantita = @Quantita WHERE ID = @ID";
        private const string DELETE_PRENOTAZIONE_SERVIZIO = "DELETE FROM PrenotazioniServizi WHERE ID = @ID";



        // /////////////////////////////////////////// GETALL ///////////////////////////////////////////////////////
        public IEnumerable<PrenotazioneServizio> GetAll()
        {
            var result = new List<PrenotazioneServizio>();

            using var conn = _dbService.GetConnection();
            {
                conn.Open();
                using var command = _dbService.GetCommand(conn, GET_ALL_PRENOTAZIONI_SERVIZI);
                using var reader = command.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        result.Add(new PrenotazioneServizio
                        {
                            ID = reader.GetInt32(0),
                            NumeroPrenotazione = reader.GetInt32(1),
                            ServizioID = reader.GetInt32(2),
                            Data = reader.GetDateTime(3),
                            Quantita = reader.GetInt32(4)
                        });
                    }
                }
                conn.Close();
            }

            return result;
        }
        // /////////////////////////////////////////// FET BY ID ///////////////////////////////////////////////////////
        public PrenotazioneServizio GetById(int id)
        {
            PrenotazioneServizio prenotazioneServizio = null;

            using var conn = _dbService.GetConnection();
            {
                conn.Open();
                using var command = _dbService.GetCommand(conn, GET_PRENOTAZIONE_SERVIZIO_BY_ID);
                {
                    command.Parameters.Add(new SqlParameter("@ID", id));

                    using var reader = command.ExecuteReader();
                    {
                        if (reader.Read())
                        {
                            prenotazioneServizio = new PrenotazioneServizio
                            {
                                ID = reader.GetInt32(0),
                                NumeroPrenotazione = reader.GetInt32(1),
                                ServizioID = reader.GetInt32(2),
                                Data = reader.GetDateTime(3),
                                Quantita = reader.GetInt32(4)
                            };
                        }
                    }
                }
                conn.Close();
            }

            return prenotazioneServizio;
        }
        // /////////////////////////////////////////// ADD ///////////////////////////////////////////////////////
        public void Add(PrenotazioneServizio prenotazioneServizio)
        {
            using var conn = _dbService.GetConnection();
            {
                conn.Open();
                using var transaction = conn.BeginTransaction();
                {
                    try
                    {
                        using var command = _dbService.GetCommand(conn, CREATE_PRENOTAZIONE_SERVIZIO);
                        
                            command.Transaction = transaction;
                            command.Parameters.Add(new SqlParameter("@NumeroPrenotazione", prenotazioneServizio.NumeroPrenotazione));
                            command.Parameters.Add(new SqlParameter("@ServizioID", prenotazioneServizio.ServizioID));
                            command.Parameters.Add(new SqlParameter("@Data", prenotazioneServizio.Data));
                            command.Parameters.Add(new SqlParameter("@Quantita", prenotazioneServizio.Quantita));

                            prenotazioneServizio.ID = (int)command.ExecuteScalar();
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
        // /////////////////////////////////////////// UPDATE ///////////////////////////////////////////////////////
        public void Update(PrenotazioneServizio prenotazioneServizio)
        {
            using var conn = _dbService.GetConnection();
            {
                conn.Open();
                using var transaction = conn.BeginTransaction();
                
                    try
                    {
                        using var command = _dbService.GetCommand(conn, UPDATE_PRENOTAZIONE_SERVIZIO);
                        
                            command.Transaction = transaction;
                            command.Parameters.Add(new SqlParameter("@ID", prenotazioneServizio.ID));
                            command.Parameters.Add(new SqlParameter("@NumeroPrenotazione", prenotazioneServizio.NumeroPrenotazione));
                            command.Parameters.Add(new SqlParameter("@ServizioID", prenotazioneServizio.ServizioID));
                            command.Parameters.Add(new SqlParameter("@Data", prenotazioneServizio.Data));
                            command.Parameters.Add(new SqlParameter("@Quantita", prenotazioneServizio.Quantita));

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
            using var conn = _dbService.GetConnection();
            {
                conn.Open();
                using var transaction = conn.BeginTransaction();
                
                    try
                    {
                        using (var command = _dbService.GetCommand(conn, DELETE_PRENOTAZIONE_SERVIZIO))
                        {
                            command.Transaction = transaction;
                            command.Parameters.Add(new SqlParameter("@ID", id));
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
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
