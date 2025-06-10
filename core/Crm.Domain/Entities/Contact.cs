namespace Crm.Domain.Entities;
public class Contact : BaseEntity
{
	// Private backing fields
	private readonly List<Deal> _deals = new();
	private readonly List<TaskItem> _tasks = new();
	private readonly List<Note> _notes = new();
	private readonly List<Activity> _activities = new();
	private readonly List<Lead> _leads = new();

	// Properties
	public string FirstName { get; private set; }
	public string LastName { get; private set; }
	public Email Email { get; private set; }
	public PhoneNumber Phone { get; private set; }
	public string JobTitle { get; private set; }
	public string Department { get; private set; }
	public bool IsPrimary { get; private set; }

	// Foreign keys
	public Guid CompanyId { get; private set; }
	public Guid UserId { get; private set; }

	// Navigation properties
	public Company Company { get; private set; }
	public User User { get; private set; }
	public IReadOnlyCollection<Deal> Deals => _deals.AsReadOnly();
	public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();
	public IReadOnlyCollection<Note> Notes => _notes.AsReadOnly();
	public IReadOnlyCollection<Activity> Activities => _activities.AsReadOnly();
	public IReadOnlyCollection<Lead> Leads => _leads.AsReadOnly();

	// Private constructor for EF Core
	private Contact() { }

	// Constructor for creation
	public Contact(
		string firstName,
		string lastName,
		Email email,
		PhoneNumber phone,
		Guid companyId,
		Guid userId,
		string jobTitle = null,
		string department = null)
	{
		ValidateName(firstName, lastName);
		
		FirstName = firstName;
		LastName = lastName;
		Email = email ?? throw new ArgumentNullException(nameof(email));
		Phone = phone ?? throw new ArgumentNullException(nameof(phone));
		CompanyId = companyId;
		UserId = userId;
		JobTitle = jobTitle;
		Department = department;
		UpdatedAt = DateTime.UtcNow;

		AddDomainEvent(new ContactCreatedEvent(this));
	}

	// Business methods
	public void UpdateContactInfo(string firstName, string lastName, Email email, PhoneNumber phone)
	{
		ValidateName(firstName, lastName);
		
		FirstName = firstName;
		LastName = lastName;
		Email = email ?? throw new ArgumentNullException(nameof(email));
		Phone = phone ?? throw new ArgumentNullException(nameof(phone));
		UpdatedAt = DateTime.UtcNow;

		AddDomainEvent(new ContactInfoUpdatedEvent(this));
	}

	public void UpdateJobInfo(string jobTitle, string department)
	{
		JobTitle = jobTitle;
		Department = department;
		UpdatedAt = DateTime.UtcNow;
	}

	public void SetAsPrimary()
	{
		if (IsPrimary) return;
		
		IsPrimary = true;
		UpdatedAt = DateTime.UtcNow;
		AddDomainEvent(new ContactSetAsPrimaryEvent(this));
	}

	public void SetAsSecondary()
	{
		if (!IsPrimary) return;
		
		IsPrimary = false;
		UpdatedAt = DateTime.UtcNow;
		AddDomainEvent(new ContactSetAsSecondaryEvent(this));
	}

	// Collection management methods
	public void AddDeal(Deal deal)
	{
		if (deal == null) throw new ArgumentNullException(nameof(deal));
		_deals.Add(deal);
		UpdatedAt = DateTime.UtcNow;
	}

	public void RemoveDeal(Deal deal)
	{
		if (deal == null) throw new ArgumentNullException(nameof(deal));
		_deals.Remove(deal);
		UpdatedAt = DateTime.UtcNow;
	}

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
	private static void ValidateName(string firstName, string lastName)
	{
		if (string.IsNullOrWhiteSpace(firstName))
			throw new DomainException("First name cannot be empty");
		
		if (string.IsNullOrWhiteSpace(lastName))
			throw new DomainException("Last name cannot be empty");
		
		if (firstName.Length > 50)
			throw new DomainException("First name cannot be longer than 50 characters");
		
		if (lastName.Length > 50)
			throw new DomainException("Last name cannot be longer than 50 characters");
	}
}

// Domain Events
public class ContactCreatedEvent : DomainEventsBase
{
	public Contact Contact { get; }
	public ContactCreatedEvent(Contact contact) : base(typeof(Contact), contact.Id)
	{
		Contact = contact;
	}
}

public class ContactInfoUpdatedEvent : DomainEventsBase
{
	public Contact Contact { get; }
	public ContactInfoUpdatedEvent(Contact contact) : base(typeof(Contact), contact.Id)
	{
		Contact = contact;
	}
}

public class ContactSetAsPrimaryEvent : DomainEventsBase
{
	public Contact Contact { get; }
	public ContactSetAsPrimaryEvent(Contact contact) : base(typeof(Contact), contact.Id)
	{
		Contact = contact;
	}
}

public class ContactSetAsSecondaryEvent : DomainEventsBase
{
	public Contact Contact { get; }
	public ContactSetAsSecondaryEvent(Contact contact) : base(typeof(Contact), contact.Id)
	{
		Contact = contact;
	}
}

public class ContactTypeChangedEvent : DomainEventsBase
{
	public Contact Contact { get; }
	public ContactTypeChangedEvent(Contact contact) : base(typeof(Contact), contact.Id)
	{
		Contact = contact;
	}
}