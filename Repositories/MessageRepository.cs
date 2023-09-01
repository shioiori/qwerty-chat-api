using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwerty_chat_api.Models;
using qwerty_chat_api.Repositories.Interface;
using qwerty_T_api.Repositories;

namespace qwerty_chat_api.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        private readonly IMongoCollection<Message> _MessagesCollection;

        public MessageRepository(
            IOptions<ChatDatabaseSettings> ChatDatabaseSettings) : base(ChatDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ChatDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ChatDatabaseSettings.Value.DatabaseName);

            _MessagesCollection = mongoDatabase.GetCollection<Message>(
                ChatDatabaseSettings.Value.MessagesCollectionName);
        }

        public async Task<List<Message>> GetAllMessagesByChat(string chat_id)
        {
            return await _MessagesCollection.Find(x => x.ChatId == chat_id).ToListAsync();
        }
    }
}
