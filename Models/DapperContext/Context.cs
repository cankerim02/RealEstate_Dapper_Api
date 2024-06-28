using Microsoft.Data.SqlClient;
using System.Data;

namespace RealEstate_Dapper__Api.Models.DapperContext
{
    public class Context
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public Context(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("connection"); //app setting.json tanımladıgım key değeri verdik.
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        
    }
}
