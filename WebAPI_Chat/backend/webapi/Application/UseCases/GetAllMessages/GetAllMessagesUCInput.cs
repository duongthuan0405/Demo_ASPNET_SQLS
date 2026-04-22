using webapi.Application.UseCases.Base;

namespace webapi.Application.UseCases.GetAllMessages
{
    public class GetAllMessagesUCInput : IValidable
    {
        public Dictionary<string, List<string>>? Validate()
        {
            return null;
        }
    }
}
