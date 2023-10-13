﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwerty_chat_api.Domain.Utils;
using qwerty_chat_api.Infrastructure.Models;
using qwerty_chat_api.Infrastructure.Repositories.Interface;

namespace qwerty_chat_api.Infrastructure.Repositories
{
    public class ChatRepository : BaseRepository<Chat>, IChatRepository
    {
        private readonly IMongoCollection<Chat> _ChatsCollection;

        public ChatRepository(
            IOptions<ChatDatabaseSettings> ChatDatabaseSettings) : base(ChatDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ChatDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ChatDatabaseSettings.Value.DatabaseName);

            _ChatsCollection = mongoDatabase.GetCollection<Chat>(CollectionUtils<Chat>.GetCollectionName());
        }

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
