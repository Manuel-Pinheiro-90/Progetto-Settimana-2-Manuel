using Progetto_Settimana_2_Manuel.Models;
using Progetto_Settimana_2_Manuel.Service;
using System.Data.SqlClient;

namespace Progetto_Settimana_2_Manuel.DAO
{
    public class ClienteDAO : IClienteDAO
    {
        private readonly SqlServerServiceBase _dbService;

        public ClienteDAO(SqlServerServiceBase dbService)
        {
            _dbService = dbService;
        }

        private const string GET_ALL_CLIENTI = "SELECT * FROM Clienti";
        private const string GET_CLIENTE_BY_ID = "SELECT * FROM Clienti WHERE ID = @ID";
        private const string CREATE_CLIENTE = "INSERT INTO Clienti (CodiceFiscale, Cognome, Nome, Citta, Provincia, Email, Telefono, Cellulare) " +
                                              "OUTPUT INSERTED.ID " +
                                              "VALUES (@CodiceFiscale, @Cognome, @Nome, @Citta, @Provincia, @Email, @Telefono, @Cellulare)";
        private const string UPDATE_CLIENTE = "UPDATE Clienti SET CodiceFiscale = @CodiceFiscale, Cognome = @Cognome, Nome = @Nome, " +
                                              "Citta = @Citta, Provincia = @Provincia, Email = @Email, Telefono = @Telefono, " +
                                              "Cellulare = @Cellulare WHERE ID = @ID";
        private const string DELETE_CLIENTE = "DELETE FROM Clienti WHERE ID = @ID";

        // ///////////////////////////////////////////GET ALL////////////////////////////////////////////////////////////
        public IEnumerable<Cliente> GetAll()
        {
            var result = new List<Cliente>();

            using (var conn = _dbService.GetConnection())
            {
                conn.Open();
                using (var command = _dbService.GetCommand(conn, GET_ALL_CLIENTI))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Cliente
                        {
                            ID = reader.GetInt32(0),
                            CodiceFiscale = reader.GetString(1),
                            Cognome = reader.GetString(2),
                            Nome = reader.GetString(3),
                            Citta = reader.GetString(4),
                            Provincia = reader.GetString(5),
                            Email = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Telefono = reader.IsDBNull(7) ? null : reader.GetString(7),
                            Cellulare = reader.IsDBNull(8) ? null : reader.GetString(8)
                        });
                    }
                }
                conn.Close();
            }

            return result;
        }



        // /////////////////////////////////////////GET BY ID //////////////////////////////////////////////////////////////      

        public Cliente GetById(int id)
        {
            Cliente cliente = null;

            using (var conn = _dbService.GetConnection())
            {
                conn.Open();
                using (var command = _dbService.GetCommand(conn, GET_CLIENTE_BY_ID))
                {
                    command.Parameters.Add(new SqlParameter("@ID", id));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = new Cliente
                            {
                                ID = reader.GetInt32(0),
                                CodiceFiscale = reader.GetString(1),
                                Cognome = reader.GetString(2),
                                Nome = reader.GetString(3),
                                Citta = reader.GetString(4),
                                Provincia = reader.GetString(5),
                                Email = reader.IsDBNull(6) ? null : reader.GetString(6),
                                Telefono = reader.IsDBNull(7) ? null : reader.GetString(7),
                                Cellulare = reader.IsDBNull(8) ? null : reader.GetString(8)
                            };
                        }
                    }
                }
                conn.Close();
            }

            return cliente;
        }

        // ///////////////////////////////////////// ADD ////////////////////////////////////////////////////////////// 

        public void Add(Cliente cliente)
        {
            using var conn = _dbService.GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                using var command = _dbService.GetCommand(conn, CREATE_CLIENTE);
                command.Transaction = transaction;
                command.Parameters.Add(new SqlParameter("@CodiceFiscale", cliente.CodiceFiscale));
                command.Parameters.Add(new SqlParameter("@Cognome", cliente.Cognome));
                command.Parameters.Add(new SqlParameter("@Nome", cliente.Nome));
                command.Parameters.Add(new SqlParameter("@Citta", cliente.Citta));
                command.Parameters.Add(new SqlParameter("@Provincia", cliente.Provincia));
                command.Parameters.Add(new SqlParameter("@Email", cliente.Email ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Telefono", cliente.Telefono ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Cellulare", cliente.Cellulare ?? (object)DBNull.Value));

                cliente.ID = (int)command.ExecuteScalar();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }


        // ///////////////////////////////////////// UPDATE ////////////////////////////////////////////////////////////// 

        public void Update(Cliente cliente)
        {
            using (var conn = _dbService.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (var command = _dbService.GetCommand(conn, UPDATE_CLIENTE))
                        {
                            command.Transaction = transaction;
                            command.Parameters.Add(new SqlParameter("@ID", cliente.ID));
                            command.Parameters.Add(new SqlParameter("@CodiceFiscale", cliente.CodiceFiscale));
                            command.Parameters.Add(new SqlParameter("@Cognome", cliente.Cognome));
                            command.Parameters.Add(new SqlParameter("@Nome", cliente.Nome));
                            command.Parameters.Add(new SqlParameter("@Citta", cliente.Citta));
                            command.Parameters.Add(new SqlParameter("@Provincia", cliente.Provincia));
                            command.Parameters.Add(new SqlParameter("@Email", cliente.Email ?? (object)DBNull.Value));
                            command.Parameters.Add(new SqlParameter("@Telefono", cliente.Telefono ?? (object)DBNull.Value));
                            command.Parameters.Add(new SqlParameter("@Cellulare", cliente.Cellulare ?? (object)DBNull.Value));

                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        // Log the exception (you can use any logging framework)
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }


        // /////////////////////////////////////////// DELETE ///////////////////////////////////////////////////////
        public void Delete(int id)
        {
            using var conn = _dbService.GetConnection();
            
                conn.Open();
            using var transaction = conn.BeginTransaction();
                
                    try
                    {
                    using var command = _dbService.GetCommand(conn, DELETE_CLIENTE);
                        
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
