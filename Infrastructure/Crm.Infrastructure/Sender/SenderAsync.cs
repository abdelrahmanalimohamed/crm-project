namespace Crm.Infrastructure.Sender
{
	public class SenderAsync : ISenderAsync
	{
		private readonly ICommandDispatcher _commandDispatcher;
		private readonly IQueryDispatcher _queryDispatcher;
		public SenderAsync(
			ICommandDispatcher commandDispatcher,
			IQueryDispatcher queryDispatcher)
		{
			_commandDispatcher = commandDispatcher;
			_queryDispatcher = queryDispatcher;
		}

		public async Task<TResponse> Send<TCommand, TResponse>(
			TCommand command,
			CancellationToken cancellationToken = default)
			where TCommand : ICommand<TResponse>
		{
			return await _commandDispatcher.SendAsync<TCommand, TResponse>(
				command,
				cancellationToken);
		}
		public async Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default)
			where TCommand : ICommand
		{
			await _commandDispatcher.SendAsync(command, cancellationToken);
		}
		public async Task<TResponse> SendQuery<TRequest, TResponse>(TRequest query, CancellationToken cancellationToken = default) where TRequest : IQuery<TResponse>
		{
			return await _queryDispatcher.QueryAsync<TRequest , TResponse>(query, cancellationToken);
		}
	}
}