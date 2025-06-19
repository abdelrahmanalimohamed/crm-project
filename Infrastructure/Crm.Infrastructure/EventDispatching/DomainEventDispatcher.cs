namespace Crm.Infrastructure.EventDispatching;
public class DomainEventDispatcher : IDomainDispatcher
{
	private readonly IMediator _mediator;
	public DomainEventDispatcher(IMediator mediator)
	{
		_mediator = mediator;
	}
	public async ValueTask DispatchEvents
		(IEnumerable<DomainEventsBase> events, 
		CancellationToken cancellationToken = default)
	{
		foreach (var domainEvent in events)
		{
			await _mediator.Publish(domainEvent, cancellationToken);
		}
	}
}