namespace Crm.Domain.Entities;
public class Lead : BaseEntity
{
	// Private backing fields
	private readonly List<TaskItem> _tasks = new();
	private readonly List<Note> _notes = new();
	private readonly List<Activity> _activities = new();
	public LeadStatus LeadStatus { get; private set; }
	// Foreign key
	public Guid ContactId { get; private set; }

	// Navigation properties
	public Contact Contact { get; private set; }
	public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();
	public IReadOnlyCollection<Note> Notes => _notes.AsReadOnly();
	public IReadOnlyCollection<Activity> Activities => _activities.AsReadOnly();

	// Private constructor for EF Core
	private Lead() { }

	// Constructor for creation
	public Lead(Guid contactId)
	{
		ContactId = contactId;
		LeadStatus = LeadStatus.New;
		UpdatedAt = DateTime.UtcNow;

		AddDomainEvent(new LeadCreatedEvent(this));
	}

	public void ChangeStatus(LeadStatus newStatus)
	{
		if (LeadStatus == newStatus) return;

		LeadStatus = newStatus;
		UpdatedAt = DateTime.UtcNow;
		AddDomainEvent(new LeadStatusChangedEvent(this, LeadStatus));
	}

	public void ConvertToContact()
	{
		if (LeadStatus != LeadStatus.Qualified)
			throw new DomainException("Only qualified leads can be converted to contacts");

		LeadStatus = LeadStatus.Converted;
		UpdatedAt = DateTime.UtcNow;
		AddDomainEvent(new LeadConvertedEvent(this));
	}

	// Collection management methods
	public void AddTask(TaskItem task)
	{
		if (task == null) throw new ArgumentNullException(nameof(task));
		_tasks.Add(task);
		UpdatedAt = DateTime.UtcNow;
	}

	public void RemoveTask(TaskItem task)
	{
		if (task == null) throw new ArgumentNullException(nameof(task));
		_tasks.Remove(task);
		UpdatedAt = DateTime.UtcNow;
	}

	public void AddNote(Note note)
	{
		if (note == null) throw new ArgumentNullException(nameof(note));
		_notes.Add(note);
		UpdatedAt = DateTime.UtcNow;
	}

	public void RemoveNote(Note note)
	{
		if (note == null) throw new ArgumentNullException(nameof(note));
		_notes.Remove(note);
		UpdatedAt = DateTime.UtcNow;
	}

	public void AddActivity(Activity activity)
	{
		if (activity == null) throw new ArgumentNullException(nameof(activity));
		_activities.Add(activity);
		UpdatedAt = DateTime.UtcNow;
	}
}
// Domain Events
public class LeadCreatedEvent : DomainEventsBase
{
	public Lead Lead { get; }
	public LeadCreatedEvent(Lead lead) : base(typeof(Lead), lead.Id)
	{
		Lead = lead;
	}
}

public class LeadContactInfoUpdatedEvent : DomainEventsBase
{
	public Lead Lead { get; }
	public LeadContactInfoUpdatedEvent(Lead lead) : base(typeof(Lead), lead.Id)
	{
		Lead = lead;
	}
}

public class LeadStatusChangedEvent : DomainEventsBase
{
	public Lead Lead { get; }
	public LeadStatus NewStatus { get; }
	public LeadStatusChangedEvent(Lead lead, LeadStatus newStatus) : base(typeof(Lead), lead.Id)
	{
		Lead = lead;
		NewStatus = newStatus;
	}
}

public class LeadConvertedEvent : DomainEventsBase
{
	public Lead Lead { get; }
	public LeadConvertedEvent(Lead lead) : base(typeof(Lead), lead.Id)
	{
		Lead = lead;
	}
}