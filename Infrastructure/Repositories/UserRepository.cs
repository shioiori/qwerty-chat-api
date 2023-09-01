using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwerty_chat_api.Infrastructure.Models;
using qwerty_chat_api.Infrastructure.Repositories.Interface;

namespace qwerty_chat_api.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IMongoCollection<User> _UsersCollection;

        public UserRepository(
            IOptions<ChatDatabaseSettings> ChatDatabaseSettings) : base(ChatDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ChatDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ChatDatabaseSettings.Value.DatabaseName);

            _UsersCollection = mongoDatabase.GetCollection<User>(
                ChatDatabaseSettings.Value.UsersCollectionName);
        }
        public async Task<User> GetUserAuthenticatedAsync(string username, string password)
        {
            return await _UsersCollection.Find(x => x.Username == username && x.Password == password).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _UsersCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _UsersCollection.Find(x => x.Username == username).FirstOrDefaultAsync();

        }
        public async Task<List<User>> GetUserByPhoneOrEmail(string? search_value)
        {
            return await _UsersCollection.Find(x => string.IsNullOrEmpty(search_value)
                                                 || x.Phone.Contains(search_value)
                                                 || x.Email.Contains(search_value)).ToListAsync();
        }
    }
}
