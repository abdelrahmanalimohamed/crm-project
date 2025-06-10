namespace Crm.Domain.Entities;
public class TaskItem : BaseEntity
{
	// Properties
	public string Title { get; private set; }
	public string Description { get; private set; }
	public DateTime DueDate { get; private set; }
	public bool IsCompleted { get; private set; }
	public DateTime? CompletedAt { get; private set; }
	public TaskItemStatus Status { get; private set; }
	public int? ReminderMinutes { get; private set; }

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
	private TaskItem() { }

	// Constructor for creation
	public TaskItem(
		string title,
		string description,
		DateTime dueDate,
		Guid userId,
		int? reminderMinutes = null,
		Guid? leadId = null,
		Guid? contactId = null,
		Guid? companyId = null,
		Guid? dealId = null)
	{
		ValidateTitle(title);
		ValidateDueDate(dueDate);
		
		Title = title;
		Description = description;
		DueDate = dueDate;
		UserId = userId;
		Status = TaskItemStatus.New;
		IsCompleted = false;
		ReminderMinutes = reminderMinutes;
		LeadId = leadId;
		ContactId = contactId;
		CompanyId = companyId;
		DealId = dealId;
		UpdatedAt = DateTime.UtcNow;

		AddDomainEvent(new TaskCreatedEvent(this));
	}

	// Business methods
	public void UpdateBasicInfo(string title, string description)
	{
		ValidateTitle(title);
		
		Title = title;
		Description = description;
		UpdatedAt = DateTime.UtcNow;

		AddDomainEvent(new TaskInfoUpdatedEvent(this));
	}

	public void UpdateDueDate(DateTime newDueDate)
	{
		ValidateDueDate(newDueDate);
		
		DueDate = newDueDate;
		UpdatedAt = DateTime.UtcNow;
		AddDomainEvent(new TaskDueDateUpdatedEvent(this));
	}

	public void ChangeStatus(TaskItemStatus newStatus)
	{
		if (Status == newStatus) return;
		
		Status = newStatus;
		UpdatedAt = DateTime.UtcNow;
		AddDomainEvent(new TaskStatusChangedEvent(this, Status));

		if (newStatus == TaskItemStatus.Finished)
		{
			Complete();
		}
		else if (newStatus == TaskItemStatus.Cancelled)
		{
			Cancel();
		}
	}

	public void Complete()
	{
		if (IsCompleted) return;
		
		IsCompleted = true;
		Status = TaskItemStatus.Finished;
		CompletedAt = DateTime.UtcNow;
		UpdatedAt = DateTime.UtcNow;
		AddDomainEvent(new TaskCompletedEvent(this));
	}

	public void Cancel()
	{
		if (Status == TaskItemStatus.Cancelled) return;
		
		Status = TaskItemStatus.Cancelled;
		UpdatedAt = DateTime.UtcNow;
		AddDomainEvent(new TaskCancelledEvent(this));
	}

	public void Reopen()
	{
		if (!IsCompleted && Status != TaskItemStatus.Cancelled) return;
		
		IsCompleted = false;
		Status = TaskItemStatus.Pending;
		CompletedAt = null;
		UpdatedAt = DateTime.UtcNow;
		AddDomainEvent(new TaskReopenedEvent(this));
	}

	public void SetReminder(int? minutes)
	{
		if (minutes.HasValue && minutes.Value <= 0)
			throw new DomainException("Reminder minutes must be positive");

		ReminderMinutes = minutes;
		UpdatedAt = DateTime.UtcNow;
		AddDomainEvent(new TaskReminderSetEvent(this));
	}

	// Private validation methods
	private static void ValidateTitle(string title)
	{
		if (string.IsNullOrWhiteSpace(title))
			throw new DomainException("Task title cannot be empty");
		
		if (title.Length > 200)
			throw new DomainException("Task title cannot be longer than 200 characters");
	}

	private static void ValidateDueDate(DateTime dueDate)
	{
		if (dueDate < DateTime.UtcNow)
			throw new DomainException("Due date cannot be in the past");
	}
}

// Domain Events
public class TaskCreatedEvent : DomainEventsBase
{
	public TaskItem Task { get; }
	public TaskCreatedEvent(TaskItem task) : base(typeof(TaskItem), task.Id)
	{
		Task = task;
	}
}

public class TaskInfoUpdatedEvent : DomainEventsBase
{
	public TaskItem Task { get; }
	public TaskInfoUpdatedEvent(TaskItem task) : base(typeof(TaskItem), task.Id)
	{
		Task = task;
	}
}

public class TaskDueDateUpdatedEvent : DomainEventsBase
{
	public TaskItem Task { get; }
	public TaskDueDateUpdatedEvent(TaskItem task) : base(typeof(TaskItem), task.Id)
	{
		Task = task;
	}
}

public class TaskStatusChangedEvent : DomainEventsBase
{
	public TaskItem Task { get; }
	public TaskItemStatus NewStatus { get; }
	public TaskStatusChangedEvent(TaskItem task, TaskItemStatus newStatus) : base(typeof(TaskItem), task.Id)
	{
		Task = task;
		NewStatus = newStatus;
	}
}

public class TaskCompletedEvent : DomainEventsBase
{
	public TaskItem Task { get; }
	public TaskCompletedEvent(TaskItem task) : base(typeof(TaskItem), task.Id)
	{
		Task = task;
	}
}

public class TaskCancelledEvent : DomainEventsBase
{
	public TaskItem Task { get; }
	public TaskCancelledEvent(TaskItem task) : base(typeof(TaskItem), task.Id)
	{
		Task = task;
	}
}

public class TaskReopenedEvent : DomainEventsBase
{
	public TaskItem Task { get; }
	public TaskReopenedEvent(TaskItem task) : base(typeof(TaskItem), task.Id)
	{
		Task = task;
	}
}

public class TaskReminderSetEvent : DomainEventsBase
{
	public TaskItem Task { get; }
	public TaskReminderSetEvent(TaskItem task) : base(typeof(TaskItem), task.Id)
	{
		Task = task;
	}
}