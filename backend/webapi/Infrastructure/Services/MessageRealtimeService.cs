using Microsoft.AspNetCore.SignalR;
using webapi.Application.Services;
using webapi.Application.UseCases.EntitiesResponses;
using webapi.Entities;
using webapi.WebAPI.SignalRHubs;

namespace webapi.Infrastructure.Services
{
    public class MessageRealtimeService : IMessageRealtimeService
    {
        private readonly IHubContext<ChatHub> _hub;

        public MessageRealtimeService(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }

        public async Task BroadcastMessageAsync(Message message)
        {
            MessageResponse messageResponse = new MessageResponse()
            {
                Id = message.Id,
                Content = message.Content,
                CreatedAt = message.CreatedAt,
                SenderId = message.UserId,
                Sender = new()
                {
                    Id = message.Sender.Id,
                    Username = message.Sender.Username,
                    FullName = message.Sender.FullName,
                }
            };
            await _hub.Clients.All.SendAsync("ReceiveMessage", messageResponse);
        }
    }
}
