using webapi.Application.UseCases.EntitiesResponses;
using webapi.Entities;

namespace webapi.Application.UseCases.GetAllMessages
{
    public class GetAllMessagesUCOutput
    {
        public List<MessageResponse> Messages { get; set; } = new List<MessageResponse>();
    }
}
