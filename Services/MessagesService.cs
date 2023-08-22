using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwerty_chat_api.Models;
using qwerty_chat_api.Repositories.Interface;
using qwerty_chat_api.Services.Interface;

namespace qwerty_chat_api.Services
{
    public class MessagesService : IMessage
    {
        private readonly IMessageRepository _messageRepository;

        public MessagesService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task CreateAsync(Message obj)
        {
            await _messageRepository.CreateAsync(obj);
        }

        public Task<List<Message>> GetAllAsync()
        {
            return _messageRepository.GetAllAsync();
        }

        public Task<List<Message>> GetAllMessagesByChat(string chat_id)
        {
            return _messageRepository.GetAllMessagesByChat(chat_id);
        }

        public Task<Message> GetAsync(string id)
        {
            return _messageRepository.GetAsync(id);
        }

        public async Task RemoveAsync(string id)
        {
            await _messageRepository.RemoveAsync(id);
        }

        public async Task StoredMessage(string id)
        {
            var message = await GetAsync(id);
            message.IsStored = true;
            await _messageRepository.UpdateAsync(id, message);
        }

        public async Task UpdateAsync(string id, Message obj)
        {
            await _messageRepository.UpdateAsync(id, obj);
        }
    }
}
