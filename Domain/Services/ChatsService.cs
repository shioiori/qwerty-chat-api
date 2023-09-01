using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using qwerty_chat_api.Domain.Services.Interface;
using qwerty_chat_api.Infrastructure.Models;
using qwerty_chat_api.Infrastructure.Repositories.Interface;

namespace qwerty_chat_api.Domain.Services
{
    public class ChatsService : IChat
    {
        private readonly IChatRepository _chatRepository;

        public ChatsService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task CreateAsync(Chat obj)
        {
            await _chatRepository.CreateAsync(obj);
        }

        public Task<List<Chat>> GetAllAsync()
        {
            return _chatRepository.GetAllAsync();
        }

        public Task<Chat> GetAsync(string id)
        {
            return _chatRepository.GetAsync(id);
        }

        public Task<Chat> GetChatByInfoAsync(string[] members, bool isLimited)
        {
            return _chatRepository.GetChatByInfoAsync(members, isLimited);
        }

        public Task<List<Chat>> GetUserChatAsync(string user_id)
        {
            return _chatRepository.GetUserChatAsync(user_id);
        }

        public async Task RemoveAsync(string id)
        {
            await _chatRepository.RemoveAsync(id);
        }

        public async Task StoredChat(string id)
        {
            var chat = await _chatRepository.GetAsync(id);
            chat.IsStored = true;
            await _chatRepository.UpdateAsync(id, chat);
        }

        public async Task UpdateAsync(string id, Chat obj)
        {
            await _chatRepository.UpdateAsync(id, obj);
        }
    }
}
