
using webapi.Application.Repositories;
using webapi.Application.UseCases.EntitiesResponses;
using webapi.Entities;

namespace webapi.Application.UseCases.GetAllMessages
{
    public class GetAllMessagesUC : IGetAllMessagesUC
    {
        private readonly IMessageRepository _messageRepository;

        public GetAllMessagesUC(IMessageRepository repo)
        {
            _messageRepository = repo;
        }

        public async Task<GetAllMessagesUCOutput> ExecuteAsync(GetAllMessagesUCInput input)
        {
            var messages = await _messageRepository.GetAllAsync();
            List<MessageResponse> messagesResponse = messages.Select(m => new MessageResponse
            {
                Id = m.Id,
                SenderId = m.UserId,
                Content = m.Content,
                CreatedAt = m.CreatedAt,
                Sender = new UserResponse
                {
                    Id = m.Sender.Id,
                    FullName = m.Sender.FullName,
                    Username = m.Sender.Username
                }
            }).ToList();


            return new GetAllMessagesUCOutput
            {
                Messages = messagesResponse
            };
        }
    }
}
