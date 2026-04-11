using Microsoft.AspNetCore.SignalR;
using webapi.Application.Services;
using webapi.Application.UseCases.SendMessage;
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

        public async Task BroadcastMessageAsync(SendMessageUCOutput message)
        {
            await _hub.Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
