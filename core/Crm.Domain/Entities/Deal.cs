namespace Crm.Domain.Entities;
public class Deal : BaseEntity
{
	// Private backing fields
	private readonly List<TaskItem> _tasks = new();
	private readonly List<Note> _notes = new();
	private readonly List<Activity> _activities = new();

	// Properties
	public string Title { get; private set; }
	public decimal Amount { get; private set; }
	public DealStages Stage { get; private set; }
	public DateTime? ExpectedCloseDate { get; private set; }
	public string Description { get; private set; }

	// Foreign keys
	public Guid ContactId { get; private set; }
	public Guid CompanyId { get; private set; }
	public Guid UserId { get; private set; }

	// Navigation properties
	public Contact Contact { get; private set; }
	public Company Company { get; private set; }
	public User User { get; private set; }
	public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();
	public IReadOnlyCollection<Note> Notes => _notes.AsReadOnly();
	public IReadOnlyCollection<Activity> Activities => _activities.AsReadOnly();

	// Private constructor for EF Core
	private Deal() { }

	// Constructor for creation
	public Deal(
		string title,
		decimal amount,
		Guid contactId,
		Guid companyId,
		Guid userId,
		string description = null,
		DateTime? expectedCloseDate = null)
	{
		ValidateTitle(title);
		ValidateAmount(amount);
		
		Title = title;
		Amount = amount;
		ContactId = contactId;
		CompanyId = companyId;
		UserId = userId;
		Stage = DealStages.Negotiation;
		Description = description;
		ExpectedCloseDate = expectedCloseDate;
		UpdatedAt = DateTime.UtcNow;

		AddDomainEvent(new DealCreatedEvent(this));
	}

	// Business methods
	public void UpdateBasicInfo(string title, string description, decimal amount)
	{
		ValidateTitle(title);
		ValidateAmount(amount);
		
		Title = title;
		Description = description;
		Amount = amount;
		UpdatedAt = DateTime.UtcNow;

		AddDomainEvent(new DealInfoUpdatedEvent(this));
	}

	public void ChangeStage(DealStages newStage)
	{
		if (Stage == newStage) return;

		Stage = newStage;
		UpdatedAt = DateTime.UtcNow;
		AddDomainEvent(new DealStageChangedEvent(this, Stage));
	}
	public void UpdateExpectedCloseDate(DateTime? newDate)
	{
		if (ExpectedCloseDate == newDate) return;

		ExpectedCloseDate = newDate;
		UpdatedAt = DateTime.UtcNow;
		AddDomainEvent(new DealCloseDateUpdatedEvent(this));
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

	// Private validation methods
	private static void ValidateTitle(string title)
	{
		if (string.IsNullOrWhiteSpace(title))
			throw new DomainException("Deal title cannot be empty");
		
		if (title.Length > 200)
			throw new DomainException("Deal title cannot be longer than 200 characters");
	}

	private static void ValidateAmount(decimal amount)
	{
		if (amount < 0)
			throw new DomainException("Deal amount cannot be negative");
	}
}

// Domain Events
public class DealCreatedEvent : DomainEventsBase
{
	public Deal Deal { get; }
	public DealCreatedEvent(Deal deal) : base(typeof(Deal), deal.Id)
	{
		Deal = deal;
	}
}

public class DealInfoUpdatedEvent : DomainEventsBase
{
	public Deal Deal { get; }
	public DealInfoUpdatedEvent(Deal deal) : base(typeof(Deal), deal.Id)
	{
		Deal = deal;
	}
}

public class DealStageChangedEvent : DomainEventsBase
{
	public Deal Deal { get; }
	public DealStages NewStage { get; }
	public DealStageChangedEvent(Deal deal, DealStages newStage) : base(typeof(Deal), deal.Id)
	{
		Deal = deal;
		NewStage = newStage;
	}
}

public class DealCloseDateUpdatedEvent : DomainEventsBase
{
	public Deal Deal { get; }
	public DealCloseDateUpdatedEvent(Deal deal) : base(typeof(Deal), deal.Id)
	{
		Deal = deal;
	}
}

public class DealWonEvent : DomainEventsBase
{
	public Deal Deal { get; }
	public DealWonEvent(Deal deal) : base(typeof(Deal), deal.Id)
	{
		Deal = deal;
	}
}

public class DealLostEvent : DomainEventsBase
{
	public Deal Deal { get; }
	public DealLostEvent(Deal deal) : base(typeof(Deal), deal.Id)
	{
		Deal = deal;
	}
}