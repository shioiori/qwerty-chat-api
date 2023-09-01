using qwerty_chat_api.Infrastructure.Models;

namespace qwerty_chat_api.Infrastructure.Repositories.Interface
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        Task<List<Message>> GetAllMessagesByChat(string chat_id);
    }
}
