using Common.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog.Core;
using Serilog.Events;

namespace Common.Logs.Serilog
{
    public class ElasticToSqlServerFailureSink : ILogEventSink
    {
        private readonly string _tableName;
        private readonly string _connectionString;
        private IConfiguration Configuration => new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile($"serilog.{GeneralUtilities.GetEnvironment()}.json")
        .AddJsonFile($"serilog.json")
        .Build();

        public ElasticToSqlServerFailureSink()
        {
            _connectionString = Configuration.GetValue<string>("Serilog:WriteTo:1:Args:connectionString");
            _tableName = Configuration.GetValue<string>("Serilog:WriteTo:1:Args:tableName") + "Failure";

            EnsureTableExists();
        }
        public void Emit(LogEvent logEvent)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO {_tableName} (Timestamp, Message, Level, Exception,LogEvent) VALUES (@timestamp, @message, @level, @exception,@logEvent)";
                command.Parameters.AddWithValue("@timestamp", logEvent.Timestamp);
                command.Parameters.AddWithValue("@message", logEvent.RenderMessage());
                command.Parameters.AddWithValue("@level", logEvent.Level.ToString());
                command.Parameters.AddWithValue("@exception", logEvent.Exception?.ToString() ?? "");
                command.Parameters.AddWithValue("@logEvent", JsonConvert.SerializeObject(logEvent.Properties)?.ToString() ?? "");
                command.ExecuteNonQuery();
            }
            catch (Exception sqlEx)
            {

                Console.WriteLine($"Failed to write to SQL Server: {sqlEx}");
            }
            
        }
        private void EnsureTableExists()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = $"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{_tableName}') " +
                                      $"CREATE TABLE {_tableName} (Id INT IDENTITY(1,1) PRIMARY KEY,Message NVARCHAR(MAX),Level NVARCHAR(20), Timestamp DATETIME,Exception NVARCHAR(MAX),LogEvent NVARCHAR(MAX))";
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ensuring table exists: {ex}");
            }
        }
    }
}
