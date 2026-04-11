using FluentValidation;
using FluentValidation.Results;

namespace webapi.Application.ClassExtensions
{
    public static class ValidatorExtensions
    {
        public static Dictionary<string, List<string>> GetErrorMessages(this IList<ValidationFailure> failures)
        {
        
            var errors = failures.GroupBy(f => f.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(gi => gi.ErrorMessage).ToList()
                );
            return errors;

        }
    }
}
