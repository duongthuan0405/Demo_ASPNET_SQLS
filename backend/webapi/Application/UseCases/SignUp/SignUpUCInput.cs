using webapi.Application.ClassExtensions;
using webapi.Application.UseCases.Base;
using webapi.Application.Validators;

namespace webapi.Application.UseCases.SignUp
{
    public class SignUpUCInput : IValidable
    {
        public string FullName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public Dictionary<string, List<string>>? Validate()
        {
            var result = SignUpUCInputValidator.Instance.Validate(this);
            if(result.IsValid)
            {
                return null;
            }
            return result.Errors.GetErrorMessages();
        }
    }
}
