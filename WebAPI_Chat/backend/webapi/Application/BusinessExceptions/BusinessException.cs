namespace webapi.Application.BusinessExceptions
{
    public class BusinessException : Exception
    {
        private Dictionary<string, List<string>>? _errors = null;
        public BusinessException(string message, Dictionary<string, List<string>>? errors) : base(message)
        {
            _errors = errors;
        }

        public Dictionary<string, List<string>>? GetErrors()
        {
            return _errors;
        }
    }
}
