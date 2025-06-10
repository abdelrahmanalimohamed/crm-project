namespace Crm.Domain.Entities;
public class Company : BaseEntity
{
	// Private backing fields
	private readonly List<Contact> _contacts = new();
	private readonly List<Deal> _deals = new();
	private readonly List<TaskItem> _tasks = new();
	private readonly List<Note> _notes = new();
	private readonly List<Activity> _activities = new();

	// Properties
	public string Name { get; private set; }
	public string Industry { get; private set; }
	public Address Address { get; private set; }
	public Email Email { get; private set; }
	public PhoneNumber PhoneNumber { get; private set; }
	public string Website { get; private set; }
	public string Description { get; private set; }

	// Navigation properties
	public IReadOnlyCollection<Contact> Contacts => _contacts.AsReadOnly();
	public IReadOnlyCollection<Deal> Deals => _deals.AsReadOnly();
	public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();
	public IReadOnlyCollection<Note> Notes => _notes.AsReadOnly();
	public IReadOnlyCollection<Activity> Activities => _activities.AsReadOnly();

	// Private constructor for EF Core
	private Company() { }

	// Constructor for creation
	public Company(
		string name,
		string industry,
		Address address,
		Email email,
		PhoneNumber phoneNumber,
		string website = null,
		string description = null)
	{
		ValidateName(name);
		
		Name = name;
		Industry = industry;
		Address = address ?? throw new ArgumentNullException(nameof(address));
		Email = email ?? throw new ArgumentNullException(nameof(email));
		PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
		Website = website;
		Description = description;
		UpdatedAt = DateTime.UtcNow;

		AddDomainEvent(new CompanyCreatedEvent(this));
	}

	// Business methods
	public void UpdateBasicInfo(string name, string industry, string description)
	{
		ValidateName(name);
		
		Name = name;
		Industry = industry;
		Description = description;
		UpdatedAt = DateTime.UtcNow;

		AddDomainEvent(new CompanyInfoUpdatedEvent(this));
	}

	public void UpdateContactInfo(Address address, Email email, PhoneNumber phoneNumber, string website)
	{
		Address = address ?? throw new ArgumentNullException(nameof(address));
		Email = email ?? throw new ArgumentNullException(nameof(email));
		PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
		Website = website;
		UpdatedAt = DateTime.UtcNow;

		AddDomainEvent(new CompanyContactInfoUpdatedEvent(this));
	}
	// Collection management methods
	public void AddContact(Contact contact)
	{
		if (contact == null) throw new ArgumentNullException(nameof(contact));
		_contacts.Add(contact);
		UpdatedAt = DateTime.UtcNow;
	}
	public void RemoveContact(Contact contact)
	{
		if (contact == null) throw new ArgumentNullException(nameof(contact));
		_contacts.Remove(contact);
		UpdatedAt = DateTime.UtcNow;
	}

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
	private static void ValidateName(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new DomainException("Company name cannot be empty");
		
		if (name.Length > 100)
			throw new DomainException("Company name cannot be longer than 100 characters");
	}
}

// Domain Events
public class CompanyCreatedEvent : DomainEventsBase
{
	public Company Company { get; }
	public CompanyCreatedEvent(Company company) : base(typeof(Company), company.Id)
	{
		Company = company;
	}
}

public class CompanyInfoUpdatedEvent : DomainEventsBase
{
	public Company Company { get; }
	public CompanyInfoUpdatedEvent(Company company) : base(typeof(Company), company.Id)
	{
		Company = company;
	}
}

public class CompanyContactInfoUpdatedEvent : DomainEventsBase
{
	public Company Company { get; }
	public CompanyContactInfoUpdatedEvent(Company company) : base(typeof(Company), company.Id)
	{
		Company = company;
	}
}
public class CompanySizeChangedEvent : DomainEventsBase
{
	public Company Company { get; }
	public CompanySizeChangedEvent(Company company) : base(typeof(Company), company.Id)
	{
		Company = company;
	}
}
public class CompanyTypeChangedEvent : DomainEventsBase
{
	public Company Company { get; }
	public CompanyTypeChangedEvent(Company company) : base(typeof(Company), company.Id)
	{
		Company = company;
	}
}
public class CompanyDeactivatedEvent : DomainEventsBase
{
	public Company Company { get; }
	public CompanyDeactivatedEvent(Company company) : base(typeof(Company), company.Id)
	{
		Company = company;
	}
}
public class CompanyActivatedEvent : DomainEventsBase
{
	public Company Company { get; }
	public CompanyActivatedEvent(Company company) : base(typeof(Company), company.Id)
	{
		Company = company;
	}
}