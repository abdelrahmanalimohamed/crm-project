namespace Crm.Application.Behaviors;
public class LoggingResultCommandHandlerDecorator<TCommand, TResult> 
	: ICommandHandler<TCommand, TResult>
	where TCommand : ICommand<TResult>
{
	private readonly ICommandHandler<TCommand, TResult> _inner;
	private readonly ICustomLogger _logger;
	private readonly ILogEntryFactory _logEntryFactory;
	public LoggingResultCommandHandlerDecorator(
		ICommandHandler<TCommand , TResult> inner , 
		ICustomLogger logger ,
		ILogEntryFactory logEntryFactory)
	{
		_inner = inner;
		_logger = logger;
		_logEntryFactory = logEntryFactory;
	}
	public async ValueTask<TResult> Handle(TCommand command, CancellationToken cancellationToken)
	{

		var result = await _inner.Handle(command, cancellationToken);

		await _logger.LogAsync(_logEntryFactory.CreateLogEntry("Information", $"Handled {typeof(TCommand).Name} successfully.", null, typeof(TCommand)), cancellationToken);

		return result;
	}
}
