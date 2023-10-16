using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwerty_chat_api.Domain.Utils;
using qwerty_chat_api.Infrastructure.Models;
using qwerty_chat_api.Infrastructure.Repositories.Interface;

namespace qwerty_chat_api.Infrastructure.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {

        public MessageRepository(
            IOptions<ChatDatabaseSettings> ChatDatabaseSettings) : base(ChatDatabaseSettings)
        {
        }

        public async Task<List<Message>> GetAllMessagesByChat(string chat_id)
        {
            return await _TCollection.Find(x => x.ChatId == chat_id).ToListAsync();
        }
    }
}
