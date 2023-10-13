using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using qwerty_chat_api.Domain.Services.Interface;
using qwerty_chat_api.Infrastructure.Models;
using System.Security.Claims;

namespace qwerty_chat_api.Application.Controllers
{
    [Authorize]
    [Route("api/chat")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IChat _chatsService;
        private readonly IUser _usersService;

        public ChatsController(IChat chatsService, IUser usersService)
        {
            _chatsService = chatsService;
            _usersService = usersService;
        }

        [HttpGet]
        [Route("get-list-connection")]
        public async Task<List<Chat>> GetListConnection()
        {
            try
            {
                var user_id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var chats = await _chatsService.GetUserChatAsync(user_id);
                return chats;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("get-current-chat")]
        public async Task<Chat> GetCurrentChat(string id)
        {
            var chat = await _chatsService.GetAsync(id);
            if (chat == null)
            {
                var user_id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                CreateNewChat(new string[] { user_id, id }, null);
            }
            return chat;
        }

        [HttpPost]
        [Route("check-user-in-chat")]
        public async Task<Chat> CheckUserInChat(string[] members, bool is_limited)
        {
            var chat = await _chatsService.GetChatByInfoAsync(members, is_limited);
            return chat;
        }


        [HttpPost]
        [Route("create-new-chat")]
        public async Task<Chat> CreateNewChat(string[] member_ids, string? name, bool limit = true)
        {
            try
            {
                var chat = new Chat()
                {
                    Name = name,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    IsStored = false,
                    IsLimited = limit,
                    MemberIds = member_ids,
                    Members = await Task.WhenAll(member_ids.Select(async x => await _usersService.GetAsync(x))),
                };
                await _chatsService.CreateAsync(chat);
                return chat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPut]
        public async Task<IActionResult> StoreChat(string id)
        {
            try
            {
                var chat = await _chatsService.GetAsync(id);
                chat.IsStored = true;
                await _chatsService.UpdateAsync(id, chat);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
