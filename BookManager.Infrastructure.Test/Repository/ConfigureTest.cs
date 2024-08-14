using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BookManager.Infrastructure.Test.Repository
{
    public class ConfigureTest
    {
        protected readonly IDbConnection _dbConnection;
        protected readonly IConfiguration _configuration;

        public ConfigureTest()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            _dbConnection = new SqlConnection(_configuration.GetConnectionString("ConnectionString"));
        }
    }
}
