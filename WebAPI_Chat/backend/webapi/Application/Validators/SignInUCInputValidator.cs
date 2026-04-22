using FluentValidation;
using webapi.Application.UseCases.SignIn;

namespace webapi.Application.Validators
{
    public class SignInUCInputValidator : AbstractValidator<SignInUCInput>
    {
        private static SignInUCInputValidator _instance = null!;
        public static SignInUCInputValidator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SignInUCInputValidator();
                }
                return _instance;
            }
        }
        private SignInUCInputValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required")
                .MaximumLength(50)
                .WithMessage("Username must be <= 50 characters");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters");
        }
    }
}
