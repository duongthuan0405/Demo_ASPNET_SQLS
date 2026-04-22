namespace webapi.Application.UseCases.Base
{
    public interface IValidable
    {
        Dictionary<string, List<string>>? Validate();
    }
}
