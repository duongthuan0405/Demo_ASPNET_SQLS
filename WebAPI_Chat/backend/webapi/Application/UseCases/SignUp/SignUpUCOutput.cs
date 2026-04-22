namespace webapi.Application.UseCases.SignUp
{
    public class SignUpUCOutput
    {
        public Guid UserId { get; set; } = Guid.Empty;
        public bool IsSuccess { get; set; } = false;
    }
}
