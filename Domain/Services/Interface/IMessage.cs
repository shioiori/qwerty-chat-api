using qwerty_chat_api.Infrastructure.Models;

namespace qwerty_chat_api.Domain.Services.Interface
{
    public interface IMessage : IBaseService<Message>
    {
        Task<List<Message>> GetAllMessagesByChat(string chat_id);
        Task StoredMessage(string id);
    }
}
