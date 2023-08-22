using qwerty_chat_api.Models;

namespace qwerty_chat_api.Repositories.Interface
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        Task<List<Message>> GetAllMessagesByChat(string chat_id);
    }
}
