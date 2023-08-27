using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using qwerty_chat_api.Models;
using qwerty_chat_api.Services;
using qwerty_chat_api.Services.Interface;

namespace qwerty_chat_api.Controllers
{
    [Authorize]
    [Route("api/message")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessage _messageService;

        public MessagesController(IMessage messagesService) 
        { 
            _messageService = messagesService;
        }

        [HttpGet]
        [Route("get-messages")]
        public async Task<List<Message>> GetMessages(string chat_id)
        {
            return await _messageService.GetAllMessagesByChat(chat_id);
        }

        [HttpPost]
        [Route("create-new-message")]
        public async Task<Message> CreateNewMessage(string id, string message)
        {
            var mess = new Message()
            {
                Id = id,
                Text = message,
                CreatedDate = DateTime.UtcNow,
                UserId = id,
            };
            await _messageService.CreateAsync(mess);
            return mess;
        }

    }
}
