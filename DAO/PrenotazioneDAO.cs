using Progetto_Settimana_2_Manuel.Models;
using Progetto_Settimana_2_Manuel.Service;
using System.Data.SqlClient;

namespace Progetto_Settimana_2_Manuel.DAO
{
    public class PrenotazioneDAO : IPrenotazioneDAO

    {
        private readonly SqlServerServiceBase _dbService;

        public PrenotazioneDAO(SqlServerServiceBase dbService)
        {
            _dbService = dbService;
        }

        private const string GET_ALL_PRENOTAZIONI = "SELECT * FROM Prenotazioni";
        private const string GET_PRENOTAZIONE_BY_ID = "SELECT * FROM Prenotazioni WHERE ID = @ID";
        private const string CREATE_PRENOTAZIONE = "INSERT INTO Prenotazioni (CodiceFiscaleCliente, NumeroCamera, DataPrenotazione, Anno, PeriodoDal, PeriodoAl, CaparraConfirmatoria, TariffaApplicata, TipoSoggiorno) " +
                                                   "OUTPUT INSERTED.ID " +
                                                   "VALUES (@CodiceFiscaleCliente, @NumeroCamera, @DataPrenotazione, @Anno, @PeriodoDal, @PeriodoAl, @CaparraConfirmatoria, @TariffaApplicata, @TipoSoggiorno)";
        private const string UPDATE_PRENOTAZIONE = "UPDATE Prenotazioni SET CodiceFiscaleCliente = @CodiceFiscaleCliente, NumeroCamera = @NumeroCamera, DataPrenotazione = @DataPrenotazione, Anno = @Anno, PeriodoDal = @PeriodoDal, PeriodoAl = @PeriodoAl, CaparraConfirmatoria = @CaparraConfirmatoria, TariffaApplicata = @TariffaApplicata, TipoSoggiorno = @TipoSoggiorno WHERE ID = @ID";
        private const string DELETE_PRENOTAZIONE = "DELETE FROM Prenotazioni WHERE ID = @ID";
        private const string DELETE_PRENOTAZIONE_SERVIZI = "DELETE FROM PrenotazioniServizi WHERE NumeroPrenotazione = @NumeroPrenotazione";





        // /////////////////////////////////////////// GET ALL ///////////////////////////////////////////////////////
        public IEnumerable<Prenotazione> GetAll()
        {
            var result = new List<Prenotazione>();

             var conn = _dbService.GetConnection();
            {
                conn.Open();
                using var command = _dbService.GetCommand(conn, GET_ALL_PRENOTAZIONI);
                using var reader = command.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        result.Add(new Prenotazione
                        {
                            ID = reader.GetInt32(0),
                            CodiceFiscaleCliente = reader.GetInt32(1),
                            NumeroCamera = reader.GetInt32(2),
                            DataPrenotazione = reader.GetDateTime(3),
                            Anno = reader.GetInt32(4),
                            PeriodoDal = reader.GetDateTime(5),
                            PeriodoAl = reader.GetDateTime(6),
                            CaparraConfirmatoria = reader.IsDBNull(7) ? (decimal?)null : reader.GetDecimal(7),
                            TariffaApplicata = reader.GetDecimal(8),
                            TipoSoggiorno = reader.GetString(9)
                        });
                    }
                }
                conn.Close();
            }

            return result;
        }

        // /////////////////////////////////////////// GET BY ID ///////////////////////////////////////////////////////
        public Prenotazione GetById(int id)
        {
            Prenotazione prenotazione = null;

            var conn = _dbService.GetConnection();
            {
                conn.Open();
                using var command = _dbService.GetCommand(conn, GET_PRENOTAZIONE_BY_ID);
                {
                    command.Parameters.Add(new SqlParameter("@ID", id));

                    using var reader = command.ExecuteReader();
                    {
                        if (reader.Read())
                        {
                            prenotazione = new Prenotazione
                            {
                                ID = reader.GetInt32(0),
                                CodiceFiscaleCliente = reader.GetInt32(1),
                                NumeroCamera = reader.GetInt32(2),
                                DataPrenotazione = reader.GetDateTime(3),
                                Anno = reader.GetInt32(4),
                                PeriodoDal = reader.GetDateTime(5),
                                PeriodoAl = reader.GetDateTime(6),
                                CaparraConfirmatoria = reader.IsDBNull(7) ? (decimal?)null : reader.GetDecimal(7),
                                TariffaApplicata = reader.GetDecimal(8),
                                TipoSoggiorno = reader.GetString(9)
                            };
                        }
                    }
                }
                conn.Close();
            }

            return prenotazione;
        }

        // /////////////////////////////////////////// ADD ///////////////////////////////////////////////////////
        public void Add(Prenotazione prenotazione)
        {
           var conn = _dbService.GetConnection();
            {
                conn.Open();
                using var transaction = conn.BeginTransaction();
                
                    try
                    {
                        using var command = _dbService.GetCommand(conn, CREATE_PRENOTAZIONE);
                        
                            command.Transaction = transaction;
                            command.Parameters.Add(new SqlParameter("@CodiceFiscaleCliente", prenotazione.CodiceFiscaleCliente));
                            command.Parameters.Add(new SqlParameter("@NumeroCamera", prenotazione.NumeroCamera));
                            command.Parameters.Add(new SqlParameter("@DataPrenotazione", prenotazione.DataPrenotazione));
                            command.Parameters.Add(new SqlParameter("@Anno", prenotazione.Anno));
                            command.Parameters.Add(new SqlParameter("@PeriodoDal", prenotazione.PeriodoDal));
                            command.Parameters.Add(new SqlParameter("@PeriodoAl", prenotazione.PeriodoAl));
                            command.Parameters.Add(new SqlParameter("@CaparraConfirmatoria", prenotazione.CaparraConfirmatoria ?? (object)DBNull.Value));
                            command.Parameters.Add(new SqlParameter("@TariffaApplicata", prenotazione.TariffaApplicata));
                            command.Parameters.Add(new SqlParameter("@TipoSoggiorno", prenotazione.TipoSoggiorno));

                            prenotazione.ID = (int)command.ExecuteScalar();
                            transaction.Commit();
                        
                    }
                    catch (Exception )
                    {
                        transaction.Rollback();
                        
                        throw;
                    }
                finally { conn.Close(); }

            }
            
        }


        // /////////////////////////////////////////// UPDATE ///////////////////////////////////////////////////////
        public void Update(Prenotazione prenotazione)
        {
            using var conn = _dbService.GetConnection();
            {
                conn.Open();
                using var transaction = conn.BeginTransaction();
                {
                    try
                    {
                        using var command = _dbService.GetCommand(conn, UPDATE_PRENOTAZIONE);
                        
                            command.Transaction = transaction;
                            command.Parameters.Add(new SqlParameter("@ID", prenotazione.ID));
                            command.Parameters.Add(new SqlParameter("@CodiceFiscaleCliente", prenotazione.CodiceFiscaleCliente));
                            command.Parameters.Add(new SqlParameter("@NumeroCamera", prenotazione.NumeroCamera));
                            command.Parameters.Add(new SqlParameter("@DataPrenotazione", prenotazione.DataPrenotazione));
                            command.Parameters.Add(new SqlParameter("@Anno", prenotazione.Anno));
                            command.Parameters.Add(new SqlParameter("@PeriodoDal", prenotazione.PeriodoDal));
                            command.Parameters.Add(new SqlParameter("@PeriodoAl", prenotazione.PeriodoAl));
                            command.Parameters.Add(new SqlParameter("@CaparraConfirmatoria", prenotazione.CaparraConfirmatoria ?? (object)DBNull.Value));
                            command.Parameters.Add(new SqlParameter("@TariffaApplicata", prenotazione.TariffaApplicata));
                            command.Parameters.Add(new SqlParameter("@TipoSoggiorno", prenotazione.TipoSoggiorno));

                            command.ExecuteNonQuery();
                            transaction.Commit();
                        
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                       
                        throw;
                    }
                   
                }
            }
        }

        // /////////////////////////////////////////// UPDATE ///////////////////////////////////////////////////////

        public void Delete(int id)
        {
            using var conn = _dbService.GetConnection();
            {
                conn.Open();
                using var transaction = conn.BeginTransaction();
                try
                {
                    //  servizi associati alla prenotazione
                    using (var command = _dbService.GetCommand(conn, DELETE_PRENOTAZIONE_SERVIZI))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add(new SqlParameter("@NumeroPrenotazione", id));
                        command.ExecuteNonQuery();
                    }

                    //  prenotazione
                    using (var command = _dbService.GetCommand(conn, DELETE_PRENOTAZIONE))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add(new SqlParameter("@ID", id));
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }




    }


}
