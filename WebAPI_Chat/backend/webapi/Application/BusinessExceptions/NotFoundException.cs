
namespace webapi.Application.BusinessExceptions
{
    public class NotFoundException : BusinessException
    {
        public NotFoundException(string message, Dictionary<string, List<string>>? errors) : base(message, errors)
        {
        }
    }
}
