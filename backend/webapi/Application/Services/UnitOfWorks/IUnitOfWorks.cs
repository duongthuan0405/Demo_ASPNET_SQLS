namespace webapi.Application.Services.UnitOfWorks
{
    public interface IUnitOfWorks
    {
        public Task BeginTransaction();
        public Task FinishTransaction();
        public Task RollbackTransaction();

    }
}
