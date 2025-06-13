namespace Crm.Domain.Repository;
public interface IBaseRepository<T> where T : class
{
	ValueTask<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);
	ValueTask<T> GetById(Guid Id, CancellationToken cancellationToken = default);
	ValueTask Insert(T entity, CancellationToken cancellationToken = default);
	ValueTask Update(T entity, CancellationToken cancellationToken = default);
	ValueTask Delete(T entity, CancellationToken cancellationToken = default);
	ValueTask<IEnumerable<T>> FindWithIncludeAsync
		(Expression<Func<T, bool>> predicate = null,
		List<Expression<Func<T, object>>> includes = null, 
		CancellationToken cancellationToken = default);
}