
namespace webapi.Application.BusinessExceptions
{
    public class ConflictUniqueValueException : BusinessException
    {
        public ConflictUniqueValueException(string message, Dictionary<string, List<string>>? errors) : base(message, errors)
        {
        }
    }
}
