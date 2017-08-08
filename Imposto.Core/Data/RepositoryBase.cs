using System.Configuration;
using System.Data.SqlClient;

namespace Imposto.Core.Data
{
    public class RepositoryBase
    {
        SqlConnection _Connection;
        internal SqlConnection Connection {
            get {
                return _Connection;
            } 
        }

        public RepositoryBase()
        {
            _Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TesteConnString"].ConnectionString);
        }

        public RepositoryBase(SqlConnection connection)
        {
            _Connection = connection;
        }
    }
}
