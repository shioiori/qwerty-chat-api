using qwerty_chat_api.Infrastructure.Models;

namespace qwerty_chat_api.Domain.Services.Interface
{
    public interface IChat : IBaseService<Chat>
    {
        Task<List<Chat>> GetUserChatAsync(string user_id);
        Task StoredChat(string id);
        Task<Chat> GetChatByInfoAsync(string[] members, bool isLimited);
    }
}
