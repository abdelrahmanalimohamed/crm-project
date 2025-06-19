namespace Crm.Infrastructure.CQRS;
public class CommandDispatcher : ICommandDispatcher
{
	private readonly IServiceProvider _serviceProvider;
	public CommandDispatcher(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}
	public async ValueTask SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
		where TCommand : ICommand
	{
		var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
		await handler.Handle(command, cancellationToken);
	}

	public async ValueTask<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken) 
		where TCommand : ICommand<TResult>
	{
		var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand,TResult>>();
		return await handler.Handle(command, cancellationToken);
	}
}
