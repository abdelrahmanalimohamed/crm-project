namespace Crm.Application.Abstraction;
public interface IDomainDispatcher
{
	ValueTask DispatchEvents(IEnumerable<DomainEventsBase> events, 
		CancellationToken cancellationToken = default);
}
