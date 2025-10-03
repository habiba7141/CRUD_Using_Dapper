using Microsoft.Data.SqlClient;
using System.Data;

namespace CRUDUsingDapper.Models.Data
{
    public class DapperDbContext
    { 
        private readonly IConfiguration _configuration;   
        private readonly  string connectionString;
        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            this.connectionString = this._configuration.GetConnectionString("connection");
        }
        public IDbConnection CreateConnection()=>new SqlConnection(connectionString);
    }
}
