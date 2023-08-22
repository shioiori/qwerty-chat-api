using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwerty_chat_api.Models;
using qwerty_chat_api.Repositories.Interface;

namespace qwerty_chat_api.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly IMongoCollection<Chat> _ChatsCollection;

        public ChatRepository(
            IOptions<ChatDatabaseSettings> ChatDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ChatDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ChatDatabaseSettings.Value.DatabaseName);

            _ChatsCollection = mongoDatabase.GetCollection<Chat>(
                ChatDatabaseSettings.Value.ChatsCollectionName);
        }

        public async Task<Chat> GetAsync(string id) =>
            await _ChatsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<Chat>> GetAllAsync() =>
            await _ChatsCollection.Find(_ => true).ToListAsync();

        
        public async Task CreateAsync(Chat newChat) =>
            await _ChatsCollection.InsertOneAsync(newChat);

        public async Task UpdateAsync(string id, Chat updatedChat) =>
            await _ChatsCollection.ReplaceOneAsync(x => x.Id == id, updatedChat);

        public async Task RemoveAsync(string id) =>
            await _ChatsCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<List<Chat>> GetUserChatAsync(string user_id)
        {
            return await _ChatsCollection.Find(x => x.MemberIds.Contains(user_id)).ToListAsync();
        }

        public async Task<Chat> GetChatByInfoAsync(string[] members, bool isLimited)
        {
           return await _ChatsCollection.Find(x => x.IsLimited == isLimited && x.MemberIds == members).FirstOrDefaultAsync();
        }
    }
}
