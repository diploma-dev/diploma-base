using Microsoft.EntityFrameworkCore.Storage;

namespace DiplomaProject.DatabaseSecret
{
    public interface IDatabaseSecret
    {
        string GetConnectionString();
    }

    public class DatabaseSecret : IDatabaseSecret
    {
        private string host = "localhost";
        private string port = "5432";
        private string username = "postgres";
        private string password = "652431";
        private string database = "test_DB_Diploma_test_4";
        private string minPool = "1";

        public string GetConnectionString()
        {
            return $"Host={host};Port={port};Username={username};Password={password};Database={database};MinPoolSize={minPool}";
        }
    }
}
