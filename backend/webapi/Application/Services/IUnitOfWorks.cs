namespace webapi.Application.Services
{
    public interface IUnitOfWorks
    {
        public Task BeginTransaction();
        public Task FinishTransaction();
        public Task RollbackTransaction();

    }
}
