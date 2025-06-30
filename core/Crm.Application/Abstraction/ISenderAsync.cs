namespace Crm.Application.Abstraction;
public interface ISenderAsync
{
	Task<TResponse> SendQuery<TRequest, TResponse>(TRequest query, CancellationToken cancellationToken = default)
	where TRequest : IQuery<TResponse>;

	Task<TResponse> Send<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default) 
		where TCommand : ICommand<TResponse>;

	Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default) 
		where TCommand : ICommand;
}