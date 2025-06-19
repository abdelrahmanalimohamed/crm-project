namespace Crm.Application.Abstraction;
public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
	ValueTask Handle(TCommand command, CancellationToken cancellationToken);
}
public interface ICommandHandler<in TCommand , TResult> where TCommand : ICommand<TResult>
{
	ValueTask<TResult> Handle(TCommand command, CancellationToken cancellationToken);
}