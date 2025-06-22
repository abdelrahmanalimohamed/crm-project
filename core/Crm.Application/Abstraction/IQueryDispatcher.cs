namespace Crm.Application.Abstraction;
public interface IQueryDispatcher
{
	ValueTask<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
      where TQuery : IQuery<TResult>;
}
