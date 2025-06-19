namespace Crm.Application.Abstraction;
public interface ICommandDispatcher
{
	ValueTask SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
   where TCommand : ICommand;

	ValueTask<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken)
		where TCommand : ICommand<TResult>;
}
