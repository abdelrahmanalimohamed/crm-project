namespace Crm.Application.Behaviors;
public class LogginQueryHandlerDecorator<TQuery, TResult>
		: IQueryHandler<TQuery, TResult>
		where TQuery : IQuery<TResult>
{
	private readonly IQueryHandler<TQuery, TResult> _innerHandler;
	private readonly ICustomLogger _logger;
	private readonly ILogEntryFactory _logEntryFactory;
	public LogginQueryHandlerDecorator(
		IQueryHandler<TQuery, TResult> innerHandler,
		ICustomLogger logger ,
		ILogEntryFactory logEntryFactory)
	{
		_innerHandler = innerHandler;
		_logger = logger;
		_logEntryFactory = logEntryFactory;
	}
	public async ValueTask<TResult> Handle(TQuery query, CancellationToken cancellationToken)
	{
		try
		{
			var result = await _innerHandler.Handle(query, cancellationToken);
			await _logger.LogAsync(_logEntryFactory.CreateLogEntry("Information", $"Handled {typeof(TQuery).Name} successfully.", null , typeof(TQuery)), cancellationToken);
			return result;
		}
		catch (Exception ex)
		{
			await _logger.LogAsync(_logEntryFactory.CreateLogEntry("Error", ex.Message, ex , typeof(TQuery)), cancellationToken);
			throw;
		}
	}
}