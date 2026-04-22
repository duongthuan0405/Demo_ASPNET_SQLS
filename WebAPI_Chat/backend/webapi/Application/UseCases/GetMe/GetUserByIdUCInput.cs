using webapi.Application.ClassExtensions;
using webapi.Application.UseCases.Base;
using webapi.Application.Validators;

namespace webapi.Application.UseCases.GetMe
{
    public class GetUserByIdUCInput : IValidable
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public Dictionary<string, List<string>>? Validate()
        {
            var res = GetMeUCInputValidator.Instance.Validate(this);
            if (!res.IsValid)
            {
                return res.Errors.GetErrorMessages();
            }
            return null;
        }
    }
}
