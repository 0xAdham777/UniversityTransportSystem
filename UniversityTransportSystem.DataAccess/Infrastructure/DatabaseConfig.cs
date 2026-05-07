using System.Data.SqlClient;

namespace UniversityTransportSystem.DataAccess.Infrastructure;

public static class DatabaseConfig
{
    public static string ConnectionString { get; set; } = "Server=localhost;Database=UniversityTransportDB;Trusted_Connection=True;TrustServerCertificate=True;";

}
