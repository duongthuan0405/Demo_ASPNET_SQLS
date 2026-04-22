using webapi.Application.ClassExtensions;
using webapi.Application.UseCases.Base;
using webapi.Application.Validators;

namespace webapi.Application.UseCases.SendMessage
{
    public class SendMessageUCInput : IValidable
    {
        public Guid SenderId { get; set; } = Guid.Empty;
        public string Content { get; set; } = string.Empty;

        public Dictionary<string, List<string>>? Validate()
        {
            var result = SendMessageUCInputValidator.Instance.Validate(this);
            if(result.IsValid)
            {
                return null;
            }
            return result.Errors.GetErrorMessages();
        }
    }
}
