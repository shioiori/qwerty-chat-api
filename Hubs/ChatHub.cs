using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using qwerty_chat_api.DTOs;
using qwerty_chat_api.Models;
using qwerty_chat_api.Services.Interface;
using System.Data;
using System.Security.Claims;

namespace qwerty_chat_api.Hubs
{
    public class ChatHub : Hub
    {
        // check user on off
        private static Dictionary<string, string> userStateConnections;
        private readonly IMessage _messageService;
        private readonly IUser _userService;
        private readonly IChat _chatService;

        public ChatHub(IMessage messageService, IUser userService, IChat chatService)
        {
            _messageService = messageService;
            _userService = userService;
            _chatService = chatService;
            if (userStateConnections == null)
            {
                userStateConnections = new Dictionary<string, string>();
            }
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public void AddUserToNetwork(string user_id)
        {
            userStateConnections.Add(user_id, this.Context.ConnectionId);
        }

        public async Task SendAll(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task SendSpecifiedUser(MessageRequest message)
        {
            
        }

        public async Task SendGroup(MessageRequest request)
        {
            var message = new Message();
            if (request.IsFile)
            {
                message = new Message()
                {
                    ChatId = request.ChatId,
                    File = request.Context,
                    CreatedDate = DateTime.UtcNow,
                    UserId = request.SenderId,
                };
            }
            else
            {
                message = new Message()
                {
                    ChatId = request.ChatId,
                    Text = request.Context,
                    CreatedDate = DateTime.UtcNow,
                    UserId = request.SenderId,
                };
            }
            _messageService.CreateAsync(message);
            Groups.AddToGroupAsync(userStateConnections[request.SenderId], request.GroupName);
            Groups.AddToGroupAsync(userStateConnections[request.ReceiverId], request.GroupName);
            var response = new MessageResponse() {
                Message = request.Context,
            };
            await Clients.Group(request.GroupName).SendAsync("ReceiveMessage", JsonConvert.SerializeObject(response));
        }

        public async Task AddToGroup(string groupName, string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var user = await _userService.GetAsync(userId);
            await Clients.Group(groupName).SendAsync("AddToGroup", $"{user.Username} has joined the group {groupName}");
        }

        public async Task RemoveFromGroup(string groupName, string userId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            var user = await _userService.GetAsync(userId);
            await Clients.Group(groupName).SendAsync("RemoveFromGroup", $"{user.Username} has left the group {groupName}");
        }
    }
}
