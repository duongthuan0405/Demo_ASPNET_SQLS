using FluentValidation;
using webapi.Application.UseCases.GetMe;

namespace webapi.Application.Validators
{
    public class GetMeUCInputValidator : AbstractValidator<GetUserByIdUCInput>
    {
        private static GetMeUCInputValidator _instance = null;

        public static GetMeUCInputValidator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ();
                }
                return _instance;
            }
        }

        private GetMeUCInputValidator()
        {
            RuleFor(p => p.UserId)
                .NotEmpty()
                .WithMessage("User ID must not be empty");
        }
    }
}
