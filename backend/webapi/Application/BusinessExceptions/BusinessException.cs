namespace webapi.Application.BusinessExceptions
{
    public class BusinessException : Exception
    {
        private Dictionary<string, List<string>>? _errors;
        public BusinessException(string message, Dictionary<string, List<string>>? errors) : base(message)
        {
            _errors = errors;
        }
    }
}
