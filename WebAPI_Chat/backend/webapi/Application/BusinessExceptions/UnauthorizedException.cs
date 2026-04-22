
namespace webapi.Application.BusinessExceptions
{
    public class UnauthorizedException : BusinessException
    {
        public UnauthorizedException(string message, Dictionary<string, List<string>>? errors) : base(message, errors)
        {
        }
    }
}
