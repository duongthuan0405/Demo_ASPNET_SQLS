using FluentValidation;
using webapi.Application.UseCases.SendMessage;

namespace webapi.Application.Validators
{
    public class SendMessageUCInputValidator : AbstractValidator<SendMessageUCInput>
    {
        private static SendMessageUCInputValidator _instance = null!;
        public static SendMessageUCInputValidator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SendMessageUCInputValidator();
                }
                return _instance;
            }
        }
        public SendMessageUCInputValidator()
        {
            RuleFor(x => x.SenderId)
                .NotEmpty()
                .WithMessage("SenderId is required");

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Message cannot be empty")
                .MaximumLength(500)
                .WithMessage("Message too long");
        }
    }
}
