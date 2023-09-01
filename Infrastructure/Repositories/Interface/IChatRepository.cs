using qwerty_chat_api.Infrastructure.Models;

namespace qwerty_chat_api.Infrastructure.Repositories.Interface
{
    public interface IChatRepository : IBaseRepository<Chat>
    {
        Task<List<Chat>> GetUserChatAsync(string user_id);
        Task<Chat> GetChatByInfoAsync(string[] members, bool isLimited);
    }
}
