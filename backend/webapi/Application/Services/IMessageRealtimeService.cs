using webapi.Application.UseCases.SendMessage;

namespace webapi.Application.Services
{
    public interface IMessageRealtimeService
    {
        Task BroadcastMessageAsync(SendMessageUCOutput output);
    }
}
