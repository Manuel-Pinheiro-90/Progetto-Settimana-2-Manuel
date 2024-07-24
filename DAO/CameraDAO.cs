using Progetto_Settimana_2_Manuel.Models;
using Progetto_Settimana_2_Manuel.Service;
using System.Data.SqlClient;

namespace Progetto_Settimana_2_Manuel.DAO
{
    public class CameraDAO : ICameraDAO
    {
        private readonly SqlServerServiceBase _dbService;

        public CameraDAO(SqlServerServiceBase dbService)
        {
            _dbService = dbService;
        }

        private const string GET_ALL_CAMERE = "SELECT * FROM Camere";
        private const string GET_CAMERA_BY_ID = "SELECT * FROM Camere WHERE ID = @ID";
        private const string CREATE_CAMERA = "INSERT INTO Camere (Numero, Descrizione, Tipologia) " +
                                              "OUTPUT INSERTED.ID " +
                                              "VALUES (@Numero, @Descrizione, @Tipologia)";
        private const string UPDATE_CAMERA = "UPDATE Camere SET Numero = @Numero, Descrizione = @Descrizione, Tipologia = @Tipologia WHERE ID = @ID";
        private const string DELETE_CAMERA = "DELETE FROM Camere WHERE ID = @ID";








        // /////////////////////////////////////////// GETALL ///////////////////////////////////////////////////////
        public IEnumerable<Camera> GetAll()
        {
            var result = new List<Camera>();

            using (var conn = _dbService.GetConnection())
            {
                conn.Open();
                using (var command = _dbService.GetCommand(conn, GET_ALL_CAMERE))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Camera
                        {
                            ID = reader.GetInt32(0),
                            Numero = reader.GetInt32(1),
                            Descrizione = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Tipologia = reader.GetString(3)
                        });
                    }
                }
                conn.Close();
            }

            return result;
        }

        // /////////////////////////////////////////// GET BY ID ///////////////////////////////////////////////////////
        public Camera GetById(int id)
        {
            Camera camera = null;

            using (var conn = _dbService.GetConnection())
            {
                conn.Open();
                using (var command = _dbService.GetCommand(conn, GET_CAMERA_BY_ID))
                {
                    command.Parameters.Add(new SqlParameter("@ID", id));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            camera = new Camera
                            {
                                ID = reader.GetInt32(0),
                                Numero = reader.GetInt32(1),
                                Descrizione = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Tipologia = reader.GetString(3)
                            };
                        }
                    }
                }
                conn.Close();
            }

            return camera;
        }
        // /////////////////////////////////////////// ADD ///////////////////////////////////////////////////////
        public void Add(Camera camera)
        {
            using var conn = _dbService.GetConnection();
            
                conn.Open();
            using var transaction = conn.BeginTransaction();
                
                   try
                    {
                        using var command = _dbService.GetCommand(conn, CREATE_CAMERA);
                        
                            command.Transaction = transaction;
                            command.Parameters.Add(new SqlParameter("@Numero", camera.Numero));
                            command.Parameters.Add(new SqlParameter("@Descrizione", camera.Descrizione ?? (object)DBNull.Value));
                            command.Parameters.Add(new SqlParameter("@Tipologia", camera.Tipologia));

                            camera.ID = (int)command.ExecuteScalar();
                            transaction.Commit();
                        
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                       
                        throw;
                    }
                   
                
            
        }

        // /////////////////////////////////////////// UPDATE ///////////////////////////////////////////////////////
        public void Update(Camera camera)
        {
            using var conn = _dbService.GetConnection();
            {
                conn.Open();
                using var transaction = conn.BeginTransaction();
                
                    try
                    {
                    using var command = _dbService.GetCommand(conn, UPDATE_CAMERA);
                        {
                            command.Transaction = transaction;
                            command.Parameters.Add(new SqlParameter("@ID", camera.ID));
                            command.Parameters.Add(new SqlParameter("@Numero", camera.Numero));
                            command.Parameters.Add(new SqlParameter("@Descrizione", camera.Descrizione ?? (object)DBNull.Value));
                            command.Parameters.Add(new SqlParameter("@Tipologia", camera.Tipologia));

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

        // /////////////////////////////////////////// DELETE ///////////////////////////////////////////////////////
        public void Delete(int id)
        {
            using var conn = _dbService.GetConnection();
            {
                conn.Open();
                using var transaction = conn.BeginTransaction();
                
                    try
                    {
                    using var command = _dbService.GetCommand(conn, DELETE_CAMERA);
                        
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
