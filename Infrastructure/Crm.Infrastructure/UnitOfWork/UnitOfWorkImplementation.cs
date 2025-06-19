namespace Crm.Infrastructure.UnitOfWork;
public class UnitOfWorkImplementation : IUnitOfWork
{
	private readonly ApplicationDbContext _applicationDbContext;
	private readonly Dictionary<Type, object> _repositories;
	private bool _disposed = false;
	public UnitOfWorkImplementation(ApplicationDbContext applicationDbContext)
	{
		_applicationDbContext = applicationDbContext;
		_repositories = new Dictionary<Type, object>();
	}
	public IBaseRepository<T> Repository<T>() where T : class
	{
		if (_repositories.TryGetValue(typeof(T), out var repository))
		{
			return (IBaseRepository<T>)repository;
		}

		var newRepository = new BaseRepository<T>(_applicationDbContext);
		_repositories.Add(typeof(T), newRepository);
		return newRepository;
	}
	public async ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default)
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