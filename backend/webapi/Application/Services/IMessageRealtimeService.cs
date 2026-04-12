using webapi.Application.UseCases.SendMessage;
using webapi.Entities;

namespace webapi.Application.Services
{
    public interface IMessageRealtimeService
    {
        Task BroadcastMessageAsync(Message output);
    }
}
