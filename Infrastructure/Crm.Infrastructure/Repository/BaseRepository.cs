namespace Crm.Infrastructure.Repository
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		private readonly DbContext _applicationDbContext;
		private readonly DbSet<T> _dbSet;
		public BaseRepository(DbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
			_dbSet = applicationDbContext.Set<T>();
		}
		public ValueTask Delete(T entity, CancellationToken cancellationToken = default)
		{
			_dbSet.Remove(entity);
			return ValueTask.CompletedTask;
		}
		public async ValueTask<IEnumerable<T>> FindWithIncludeAsync(
			Expression<Func<T, bool>> predicate = null, 
			List<Expression<Func<T, object>>> includes = null, 
			CancellationToken cancellationToken = default)
		{
			IQueryable<T> query = _applicationDbContext.Set<T>();

			if (predicate != null)
			{
				query = query.Where(predicate);
			}

			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			return await query.ToListAsync(cancellationToken);
		}
		public async ValueTask<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default)
		{
			return await _applicationDbContext.Set<T>().ToListAsync(cancellationToken);
		}
		public async ValueTask<T> GetById(Guid Id, CancellationToken cancellationToken = default)
		{
			var entity = await _applicationDbContext.Set<T>().FindAsync(new object[] { Id }, cancellationToken);
			return entity ?? throw new Exception($"Entity of type {typeof(T).Name} with id {Id} not found");
		}
		public async ValueTask Insert(T entity, CancellationToken cancellationToken = default)
		{
			await _applicationDbContext.Set<T>().AddAsync(entity , cancellationToken);
		}
		public ValueTask Update(T entity, CancellationToken cancellationToken = default)
		{
			_dbSet.Update(entity);
			return ValueTask.CompletedTask;
		}
	}
}