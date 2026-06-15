using Microsoft.Data.SqlClient;

namespace SalesOrderAPI.Data
{
    public class DbContextADO
    {
        private readonly IConfiguration _configuration;

        public DbContextADO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
