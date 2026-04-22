using Microsoft.EntityFrameworkCore;
using webapi.Application.Repositories;
using webapi.Entities;
using webapi.Infrastructure.Database;
using webapi.Infrastructure.Database.Models;

namespace webapi.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MyAppDbContext _dbContext;

        public MessageRepository(MyAppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Guid> AddAsync(Message message)
        {
            var dbe = new MessageDBE
            {
                UserId = message.UserId,
                Content = message.Content,
                CreatedAt = message.CreatedAt,
                UpdatedAt = message.UpdatedAt
            };

            await _dbContext.Messages.AddAsync(dbe);

            UserDBE? userDBE = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == message.UserId);

            message.Sender = new User
            {
                Id = userDBE?.Id ?? Guid.Empty,
                Username = userDBE?.Username ?? "",
                FullName = userDBE?.FullName ?? "",
            };

            return dbe.Id;
        }

        public async Task<List<Message>> GetAllAsync()
        {
            var data = await _dbContext.Messages
                .Include(x => x.Sender)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();

            return data.Select(x => new Message
            {
                Id = x.Id,
                UserId = x.UserId,
                Content = x.Content,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Sender = new User
                {
                    Id = x.Sender.Id,
                    Username = x.Sender.Username,
                    FullName = x.Sender.FullName
                }

            }).ToList();
        }
    }
}
