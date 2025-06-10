namespace Crm.Domain.Entities;
public class Activity : BaseEntity
{
	// Properties
	public ActivityTypes Type { get; private set; }
	public string Description { get; private set; }
	public DateTime ActivityDate { get; private set; }
	public bool IsCompleted { get; private set; }
	public DateTime? CompletedAt { get; private set; }

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
	private Activity() { }

	// Constructor for creation
	public Activity(
		ActivityTypes type,
		string description,
		DateTime activityDate,
		Guid userId,
		Guid? leadId = null,
		Guid? contactId = null,
		Guid? companyId = null,
		Guid? dealId = null)
	{
		ValidateDescription(description);
		ValidateActivityDate(activityDate);
		
		Type = type;
		Description = description;
		ActivityDate = activityDate;
		UserId = userId;
		IsCompleted = false;
		LeadId = leadId;
		ContactId = contactId;
		CompanyId = companyId;
		DealId = dealId;
		UpdatedAt = DateTime.UtcNow;

		AddDomainEvent(new ActivityCreatedEvent(this));
	}

	// Business methods
	public void UpdateBasicInfo(string description)
	{
		ValidateDescription(description);
		
		Description = description;
		UpdatedAt = DateTime.UtcNow;
		AddDomainEvent(new ActivityInfoUpdatedEvent(this));
	}

	public void Reschedule(DateTime newDate)
	{
		ValidateActivityDate(newDate);
		
		ActivityDate = newDate;
		UpdatedAt = DateTime.UtcNow;
		AddDomainEvent(new ActivityRescheduledEvent(this));
	}

	// Private validation methods
	private static void ValidateDescription(string description)
	{
		if (string.IsNullOrWhiteSpace(description))
			throw new DomainException("Activity description cannot be empty");
		
		if (description.Length > 1000)
			throw new DomainException("Activity description cannot be longer than 1000 characters");
	}

	private static void ValidateActivityDate(DateTime activityDate)
	{
		if (activityDate < DateTime.UtcNow)
			throw new DomainException("Activity date cannot be in the past");
	}
}

// Domain Events
public class ActivityCreatedEvent : DomainEventsBase
{
	public Activity Activity { get; }
	public ActivityCreatedEvent(Activity activity) : base(typeof(Activity), activity.Id)
	{
		Activity = activity;
	}
}

public class ActivityInfoUpdatedEvent : DomainEventsBase
{
	public Activity Activity { get; }
	public ActivityInfoUpdatedEvent(Activity activity) : base(typeof(Activity), activity.Id)
	{
		Activity = activity;
	}
}

public class ActivityRescheduledEvent : DomainEventsBase
{
	public Activity Activity { get; }
	public ActivityRescheduledEvent(Activity activity) : base(typeof(Activity), activity.Id)
	{
		Activity = activity;
	}
}

public class ActivityPriorityChangedEvent : DomainEventsBase
{
	public Activity Activity { get; }
	public ActivityPriorityChangedEvent(Activity activity) : base(typeof(Activity), activity.Id)
	{
		Activity = activity;
	}
}

public class ActivityStatusChangedEvent : DomainEventsBase
{
	public Activity Activity { get; }
	public ActivityStatusChangedEvent(Activity activity) : base(typeof(Activity), activity.Id)
	{
		Activity = activity;
	}
}

public class ActivityCompletedEvent : DomainEventsBase
{
	public Activity Activity { get; }
	public ActivityCompletedEvent(Activity activity) : base(typeof(Activity), activity.Id)
	{
		Activity = activity;
	}
}

public class ActivityCancelledEvent : DomainEventsBase
{
	public Activity Activity { get; }
	public ActivityCancelledEvent(Activity activity) : base(typeof(Activity), activity.Id)
	{
		Activity = activity;
	}
}
public class ActivityDurationUpdatedEvent : DomainEventsBase
{
	public Activity Activity { get; }
	public ActivityDurationUpdatedEvent(Activity activity) : base(typeof(Activity), activity.Id)
	{
		Activity = activity;
	}
}