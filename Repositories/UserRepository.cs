using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwerty_chat_api.Models;
using qwerty_chat_api.Repositories.Interface;

namespace qwerty_chat_api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _UsersCollection;

        public UserRepository(
            IOptions<ChatDatabaseSettings> ChatDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ChatDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ChatDatabaseSettings.Value.DatabaseName);

            _UsersCollection = mongoDatabase.GetCollection<User>(
                ChatDatabaseSettings.Value.UsersCollectionName);
        }

        public async Task<User> GetAsync(string id) =>
            await _UsersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<User>> GetAllAsync() =>
            await _UsersCollection.Find(_ => true).ToListAsync();
        public async Task CreateAsync(User newUser) =>
            await _UsersCollection.InsertOneAsync(newUser);

        public async Task UpdateAsync(string id, User updatedUser) =>
            await _UsersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(string id) =>
            await _UsersCollection.DeleteOneAsync(x => x.Id == id);


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
        public async Task<List<User>> GetUserByPhoneOrEmail(string search_value)
        {
            return await _UsersCollection.Find(x => x.Phone.Contains(search_value)
                                                 || x.Email.Contains(search_value)).ToListAsync();
        }
    }
}
