namespace qwerty_chat_api.Infrastructure.Models
{
    public class ChatDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string MessagesCollectionName { get; set; } = null!;

        public string ChatsCollectionName { get; set; } = null!;
        public string UsersCollectionName { get; set; } = null!;
    }
}
