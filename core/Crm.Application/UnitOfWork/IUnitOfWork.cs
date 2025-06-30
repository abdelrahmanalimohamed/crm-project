namespace Crm.Application.UnitOfWork;
public interface IUnitOfWork : IDisposable
{
	//IBaseRepository<T> Repository<T>() where T : class;
	ValueTask<int> CommitAsync(CancellationToken cancellationToken = default);
}