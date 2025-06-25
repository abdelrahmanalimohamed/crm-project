namespace Crm.Domain.Base;

public abstract class BaseEntity
{
	private readonly List<DomainEventsBase> _domainEvents = new();
	public Guid Id { get; protected set; } = Guid.NewGuid();
	public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
	public DateTime? UpdatedAt { get; protected set; }
	public DateTime? DeletedAt { get; protected set; }
	public bool IsDeleted { get; protected set; }
	public IReadOnlyCollection<DomainEventsBase> DomainEvents => _domainEvents.AsReadOnly();
	protected void AddDomainEvent(DomainEventsBase domainEvent)
	{
		_domainEvents.Add(domainEvent);
	}
	protected void RemoveDomainEvent(DomainEventsBase domainEvent)
	{
		_domainEvents.Remove(domainEvent);
	}
	public void ClearDomainEvents()
	{
		_domainEvents.Clear();
	}
	public void MarkAsDeleted()
	{
		IsDeleted = true;
		DeletedAt = DateTime.UtcNow;
	}
	public override bool Equals(object obj)
	{
		if (obj is not BaseEntity other)
			return false;

		if (ReferenceEquals(this, other))
			return true;

		if (GetType() != other.GetType())
			return false;

		if (Id == Guid.Empty || other.Id == Guid.Empty)
			return false;

		return Id == other.Id;
	}

	public override int GetHashCode()
	{
		return (GetType().ToString() + Id).GetHashCode();
	}

	public static bool operator ==(BaseEntity left, BaseEntity right)
	{
		if (left is null && right is null)
			return true;

		if (left is null || right is null)
			return false;

		return left.Equals(right);
	}

	public static bool operator !=(BaseEntity left, BaseEntity right)
	{
		return !(left == right);
	}
}