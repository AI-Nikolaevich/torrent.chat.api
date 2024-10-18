using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Torrent.Chat.Api.Storage.DBAccess
{
    public class DbContext 
    {
        private readonly string? _connectionString;
        public DbContext(IConfiguration config)
        {
            ArgumentNullException.ThrowIfNull(config, nameof(config));
            _connectionString = config.GetConnectionString("DBConnectStr");
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(
              string query,
              object? parameters = null,
              CommandType? commandType = null)
        {
            using IDbConnection connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<T>(new CommandDefinition(query, parameters, commandType: commandType));
        }
        public async Task<int> ExecuteAsync(
            string query,
            object? parameters = null,
            CommandType? commandType = null)
        {
            using IDbConnection connection = new NpgsqlConnection(_connectionString);
            return await connection.ExecuteAsync(new CommandDefinition(query, parameters, commandType: commandType));
        }
    }
}
