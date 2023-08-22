using qwerty_chat_api.Models;

namespace qwerty_chat_api.Repositories.Interface
{
    public interface IChatRepository : IBaseRepository<Chat>
    {
        Task<List<Chat>> GetUserChatAsync(string user_id);
        Task<Chat> GetChatByInfoAsync(string[] members, bool isLimited);
    }
}
