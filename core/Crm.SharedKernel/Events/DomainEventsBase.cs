using MediatR;

namespace Crm.SharedKernel.Events;
public class DomainEventsBase : INotification
{
	public Guid EventId { get; }
	public DateTime DateOccurred { get; protected set; }
	public Type SourceType { get; protected set; }
	public Guid SourceId { get; protected set; }

	protected DomainEventsBase(Type sourceType, Guid sourceId)
	{
		EventId = Guid.NewGuid();
		DateOccurred = DateTime.UtcNow;
		SourceType = sourceType ?? throw new ArgumentNullException(nameof(sourceType));
		SourceId = sourceId;
	}
}