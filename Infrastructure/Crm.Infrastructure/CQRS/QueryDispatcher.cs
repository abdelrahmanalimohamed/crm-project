namespace Crm.Infrastructure.CQRS;
public class QueryDispatcher : IQueryDispatcher
{
	private readonly IServiceProvider _serviceProvider;
	public QueryDispatcher(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}
	public async ValueTask<TResult> QueryAsync<TQuery, TResult>(
		TQuery query, 
		CancellationToken cancellationToken) 
		where TQuery : IQuery<TResult>
	{
		var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
		return await handler.Handle(query, cancellationToken);
	}
}