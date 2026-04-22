namespace webapi.Application.UseCases.Base
{
    public interface IUseCase<TIn, TOut> where TIn : IValidable where TOut : class
    {
        public Task<TOut> ExecuteAsync(TIn input);
    }
}
