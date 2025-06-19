namespace Crm.Application.Behaviors;
public class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
where TCommand : ICommand
{
	private readonly ICommandHandler<TCommand> _inner;
	private readonly ICustomLogger _logger;
	private readonly ILogEntryFactory _logEntryFactory;
	public LoggingCommandHandlerDecorator(
		ICommandHandler<TCommand> inner ,
		ICustomLogger logger ,
		ILogEntryFactory logEntryFactory)
	{
		_inner = inner;
		_logger = logger;
		_logEntryFactory = logEntryFactory;
	}
	public async ValueTask Handle(TCommand command, CancellationToken cancellationToken)
	{
		await _inner.Handle(command, cancellationToken);
		await _logger.LogAsync(_logEntryFactory.CreateLogEntry("Information", $"Handled {typeof(TCommand).Name} successfully.", null, typeof(TCommand)), cancellationToken);
	}
}
