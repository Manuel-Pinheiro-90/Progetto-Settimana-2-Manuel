using System.Data.Common;
using System.Data.SqlClient;

namespace Progetto_Settimana_2_Manuel.Service
{
    public class SqlServerServiceBase : ServiceBase
    {
        private  readonly IConfiguration _config;
        private SqlConnection _connection;

        public SqlServerServiceBase(IConfiguration config) 
        { 
            _config = config;   

        }

        public override DbConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_config.GetConnectionString("CON"));
            }
            return _connection;

        }

        public override DbCommand GetCommand(DbConnection connection, string commandText)
        {
            return new SqlCommand(commandText, (SqlConnection)connection);
        }

        public override void AddParameter(DbCommand command, string parameterName, object value)
        {
            if (command is SqlCommand sqlCmd)
            {
                sqlCmd.Parameters.AddWithValue(parameterName, value);
            }
        }



    }
}
