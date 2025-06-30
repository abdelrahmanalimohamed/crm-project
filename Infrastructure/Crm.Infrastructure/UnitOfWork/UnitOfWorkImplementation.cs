namespace Crm.Infrastructure.UnitOfWork;
public class UnitOfWorkImplementation : IUnitOfWork
{
	private readonly ApplicationDbContext _applicationDbContext;
	private bool _disposed = false;
	public UnitOfWorkImplementation(ApplicationDbContext applicationDbContext)
	{
		_applicationDbContext = applicationDbContext;
	}
	public async ValueTask<int> CommitAsync(CancellationToken cancellationToken = default)
	{
		return await _applicationDbContext.SaveChangesAsync(cancellationToken);
	}
	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				_applicationDbContext.Dispose();
			}
			_disposed = true;
		}
	}
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}