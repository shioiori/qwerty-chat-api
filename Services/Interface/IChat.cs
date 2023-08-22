using qwerty_chat_api.Models;

namespace qwerty_chat_api.Services.Interface
{
    public interface IChat : IBaseService<Chat>
    {
        Task<List<Chat>> GetUserChatAsync(string user_id);
        Task StoredChat(string id);
        Task<Chat> GetChatByInfoAsync(string[] members, bool isLimited);
    }
}
