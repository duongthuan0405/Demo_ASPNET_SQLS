using webapi.Application.ClassExtensions;
using webapi.Application.UseCases.Base;
using webapi.Application.Validators;

namespace webapi.Application.UseCases.SignIn
{
    public class SignInUCInput : IValidable
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Dictionary<string, List<string>>? Validate()
        {
            var res = SignInUCInputValidator.Instance.Validate(this);
            if (res.IsValid)
            {
                return null;
            }
            return res.Errors.GetErrorMessages();
        }
    }
}
