using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using qwerty_chat_api.Application.DTOs;
using qwerty_chat_api.Domain.Services.Interface;
using qwerty_chat_api.Infrastructure.Models;
using System.Data;
using System.Security.Claims;

namespace qwerty_chat_api.Domain.Hubs
{
    public class ChatHub : Hub, IDisposable
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

        public async Task OnConnectedNetwork(string user_id)
        {
            if (!userStateConnections.ContainsKey(user_id))
            {
                userStateConnections.Add(user_id, Context.ConnectionId);
            }
            else
            {
                userStateConnections[user_id] = Context.ConnectionId;
            }
            await Clients.Client(Context.ConnectionId).SendAsync("OnConnected", Context.ConnectionId);
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
            try
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
                        User = await _userService.GetAsync(request.SenderId),
                        Chat = await _chatService.GetAsync(request.ChatId),
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
                        User = await _userService.GetAsync(request.SenderId),
                        Chat = await _chatService.GetAsync(request.ChatId),
                    };
                }
                _messageService.CreateAsync(message);
                foreach (var item in request.ReceiverIds)
                {
                    if (userStateConnections.ContainsKey(item))
                    {
                        Groups.AddToGroupAsync(userStateConnections[item], request.GroupName);
                    }
                }
                await Clients.Group(request.GroupName).SendAsync("ReceiveMessage", JsonConvert.SerializeObject(message, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
            }
            catch (Exception ex)
            {
                await Clients.Group(request.GroupName).SendAsync("ReceiveMessage", "Error");
            }
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
