using Torrent.Chat.Api.Storage.DBAccess;

namespace Torrent.Chat.Api.Storage.DbRepository
{
    public class MsgRepository
    {
        private readonly DbContext _dbContext;

        public MsgRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetData<T>()
        {
            string query =  @"SELECT * FROM chat
                            ORDER BY id DESC
                            LIMIT 30;";

            return await _dbContext.QueryAsync<T>(query);
        }
        public async Task InsertDataAsync(string name, string msg)
        {
            string query = "INSERT INTO chat (username, message) VALUES (@username, @message)";
            var parameters = new { username = name, message = msg };

            int rowsAffected = await _dbContext.ExecuteAsync(query, parameters);
        }
    }
}
