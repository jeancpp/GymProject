using Microsoft.Extensions.Configuration;

namespace GymProject.Data
{
    public class dbConnection
    {
        public string ConnectionString { get; }
        public dbConnection(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("GymDbConnectionString");

        }
    }
}
