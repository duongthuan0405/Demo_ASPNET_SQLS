using FluentValidation;
using webapi.Application.UseCases.SignUp;

namespace webapi.Application.Validators
{
    public class SignUpUCInputValidator : AbstractValidator<SignUpUCInput>
    { 
        private static SignUpUCInputValidator _instance = null!;
        public static SignUpUCInputValidator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SignUpUCInputValidator();
                }
                return _instance;
            }
        }
        public SignUpUCInputValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.");
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
