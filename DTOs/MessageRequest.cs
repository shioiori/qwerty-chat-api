namespace qwerty_chat_api.DTOs
{
    public class MessageRequest
    {
        public string GroupName { get; set; }
        public string ChatId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public bool IsFile { get; set; } = false;
        public string Context { get; set; }
    }
}
