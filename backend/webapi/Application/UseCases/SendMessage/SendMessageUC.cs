
using FluentValidation;
using webapi.Application.BusinessExceptions;
using webapi.Application.ClassExtensions;
using webapi.Application.Repositories;
using webapi.Application.Services;
using webapi.Entities;

namespace webapi.Application.UseCases.SendMessage
{
    public class SendMessageUC : ISendMessageUC
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IMessageRealtimeService _realtime;

        public SendMessageUC(
            IMessageRepository repo,
            IUnitOfWorks uow,
            IMessageRealtimeService realtime)
        {
            _messageRepository = repo;
            _unitOfWorks = uow;
            _realtime = realtime;
        }

        public async Task<SendMessageUCOutput> ExecuteAsync(
            SendMessageUCInput input)
        {
            // 1. Validate
            var errors = input.Validate();
            if(errors != null)
            {
                throw new InvalidUseCasesInputException("Invalid input", errors);
            }

            // 2. Tạo message
            var message = new Message
            {
                UserId = input.SenderId,
                Content = input.Content,
                CreatedAt = DateTime.Now
            };

            // 3. Lưu DB (đơn → không cần transaction)
            Guid id = await _messageRepository.AddAsync(message);
            await _unitOfWorks.FinishTransaction();

            // 4. Map output
            var output = new SendMessageUCOutput
            {
                MessageId = id,
                UserId = message.UserId,
                Content = message.Content,
                CreatedAt = message.CreatedAt
            };

            // 5. Broadcast realtime
            await _realtime.BroadcastMessageAsync(output);

            return output;
        }
    }
}
