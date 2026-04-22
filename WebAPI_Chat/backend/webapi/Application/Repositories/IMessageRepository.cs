using webapi.Entities;

namespace webapi.Application.Repositories
{
    public interface IMessageRepository
    {
        Task<Guid> AddAsync(Message message);
        Task<List<Message>> GetAllAsync();
    }
}
