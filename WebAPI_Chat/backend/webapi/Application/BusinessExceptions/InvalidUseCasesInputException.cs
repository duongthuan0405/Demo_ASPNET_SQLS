
namespace webapi.Application.BusinessExceptions
{
    public class InvalidUseCasesInputException : BusinessException
    {
        public InvalidUseCasesInputException(string message, Dictionary<string, List<string>>? errors) : base(message, errors)
        {
        }
    }
}
