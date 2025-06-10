namespace Crm.Domain.Entities;

public class Note : BaseEntity
{
	// Properties
	public string Content { get; private set; }
	public bool IsPrivate { get; private set; }
	public bool IsPinned { get; private set; }
	public DateTime? PinnedUntil { get; private set; }

	// Foreign keys (nullable for optional relationships)
	public Guid UserId { get; private set; }
	public Guid? LeadId { get; private set; }
	public Guid? ContactId { get; private set; }
	public Guid? CompanyId { get; private set; }
	public Guid? DealId { get; private set; }

	// Navigation properties
	public User User { get; private set; }
	public Lead Lead { get; private set; }
	public Contact Contact { get; private set; }
	public Company Company { get; private set; }
	public Deal Deal { get; private set; }

	// Private constructor for EF Core
	private Note() { }

	// Constructor for creation
	public Note(
		string content,
		Guid userId,
		bool isPrivate = false,
		Guid? leadId = null,
		Guid? contactId = null,
		Guid? companyId = null,
		Guid? dealId = null)
	{
		ValidateContent(content);
		
		Content = content;
		UserId = userId;
		IsPrivate = isPrivate;
		IsPinned = false;
		LeadId = leadId;
		ContactId = contactId;
		CompanyId = companyId;
		DealId = dealId;
		UpdatedAt = DateTime.UtcNow;

		AddDomainEvent(new NoteCreatedEvent(this));
	}

	// Business methods
	public void UpdateContent(string newContent)
	{
		ValidateContent(newContent);
		
		Content = newContent;
		UpdatedAt = DateTime.UtcNow;
		AddDomainEvent(new NoteContentUpdatedEvent(this));
	}
	public void SetPrivate(bool isPrivate)
	{
		if (IsPrivate == isPrivate) return;
		
		IsPrivate = isPrivate;
		UpdatedAt = DateTime.UtcNow;
		AddDomainEvent(new NotePrivacyChangedEvent(this, IsPrivate));
	}

	public void Pin(DateTime? until = null)
	{
		if (IsPinned && PinnedUntil == until) return;
		
		IsPinned = true;
		PinnedUntil = until;
		UpdatedAt = DateTime.UtcNow;
		AddDomainEvent(new NotePinnedEvent(this));
	}

	public void Unpin()
	{
		if (!IsPinned) return;
		
		IsPinned = false;
		PinnedUntil = null;
		UpdatedAt = DateTime.UtcNow;
		AddDomainEvent(new NoteUnpinnedEvent(this));
	}

	// Private validation methods
	private static void ValidateContent(string content)
	{
		if (string.IsNullOrWhiteSpace(content))
			throw new DomainException("Note content cannot be empty");
		
		if (content.Length > 4000)
			throw new DomainException("Note content cannot be longer than 4000 characters");
	}
}

// Domain Events
public class NoteCreatedEvent : DomainEventsBase
{
	public Note Note { get; }
	public NoteCreatedEvent(Note note) : base(typeof(Note), note.Id)
	{
		Note = note;
	}
}

public class NoteContentUpdatedEvent : DomainEventsBase
{
	public Note Note { get; }
	public NoteContentUpdatedEvent(Note note) : base(typeof(Note), note.Id)
	{
		Note = note;
	}
}
public class NotePrivacyChangedEvent : DomainEventsBase
{
	public Note Note { get; }
	public bool IsPrivate { get; }
	public NotePrivacyChangedEvent(Note note, bool isPrivate) : base(typeof(Note), note.Id)
	{
		Note = note;
		IsPrivate = isPrivate;
	}
}
public class NotePinnedEvent : DomainEventsBase
{
	public Note Note { get; }
	public NotePinnedEvent(Note note) : base(typeof(Note), note.Id)
	{
		Note = note;
	}
}
public class NoteUnpinnedEvent : DomainEventsBase
{
	public Note Note { get; }
	public NoteUnpinnedEvent(Note note) : base(typeof(Note), note.Id)
	{
		Note = note;
	}
}