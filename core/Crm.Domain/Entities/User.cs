namespace Crm.Domain.Entities;

// Fix for IDE0290: Use primary constructor for the User class
public class User : BaseEntity
{
	// Private backing fields
	private readonly List<Lead> _leads = new();
	private readonly List<Contact> _contacts = new();
	private readonly List<TaskItem> _tasks = new();
	private readonly List<Note> _notes = new();
	private readonly List<Activity> _activities = new();

	// Properties
	public FullName FullName { get; private set; }
	public Email Email { get; private set; }
	public string PasswordHash { get; private set; }
	public string Salt { get; private set; }
	public Role Role { get; private set; }
	public bool IsActive { get; private set; }
	public DateTime? LastLoginAt { get; private set; }

	public IReadOnlyCollection<Lead> Leads => _leads.AsReadOnly();
	public IReadOnlyCollection<Contact> Contacts => _contacts.AsReadOnly();
	public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();
	public IReadOnlyCollection<Note> Notes => _notes.AsReadOnly();
	public IReadOnlyCollection<Activity> Activities => _activities.AsReadOnly();

	private User() { }
	// Constructor for creation
	public User(
		FullName fullName, 
		Email email, 
		string passwordHash, 
		string salt ,
		Role role)
	{
		ValidatePassword(passwordHash);

		FullName = new FullName(fullName.FirstName , fullName.LastName);
		Email = email ?? throw new ArgumentNullException(nameof(email));
		PasswordHash = passwordHash;
		Salt = salt;
		Role = role;
		IsActive = true;

		AddDomainEvent(new UserCreatedEvent(this));
	}
	private void ValidatePassword(string passwordHash)
	{
		if (string.IsNullOrWhiteSpace(passwordHash))
			throw new ArgumentException("Password hash cannot be empty.", nameof(passwordHash));

		if (passwordHash.Length < 60) // bcrypt hashes are typically 60 characters
			throw new ArgumentException("Invalid password hash length.", nameof(passwordHash));
	}
}

// Fix for CS7036: Add required parameters to the base constructor call in domain event classes
public class UserCreatedEvent : DomainEventsBase
{
	public User User { get; }
	public UserCreatedEvent(User user)
		: base(typeof(UserCreatedEvent), user.Id) // Pass sourceType and sourceId to the base constructor
	{
		User = user;
	}
}

public class UserEmailUpdatedEvent : DomainEventsBase
{
	public User User { get; }
	public UserEmailUpdatedEvent(User user)
		: base(typeof(UserEmailUpdatedEvent), user.Id) // Pass sourceType and sourceId to the base constructor
	{
		User = user;
	}
}

public class UserPasswordUpdatedEvent : DomainEventsBase
{
	public User User { get; }
	public UserPasswordUpdatedEvent(User user)
		: base(typeof(UserPasswordUpdatedEvent), user.Id) // Pass sourceType and sourceId to the base constructor
	{
		User = user;
	}
}

public class UserRoleUpdatedEvent : DomainEventsBase
{
	public User User { get; }
	public UserRoleUpdatedEvent(User user)
		: base(typeof(UserRoleUpdatedEvent), user.Id) // Pass sourceType and sourceId to the base constructor
	{
		User = user;
	}
}

public class UserDeactivatedEvent : DomainEventsBase
{
	public User User { get; }
	public UserDeactivatedEvent(User user)
		: base(typeof(UserDeactivatedEvent), user.Id) // Pass sourceType and sourceId to the base constructor
	{
		User = user;
	}
}
public class UserActivatedEvent : DomainEventsBase
{
	public User User { get; }
	public UserActivatedEvent(User user)
		: base(typeof(UserActivatedEvent), user.Id) // Pass sourceType and sourceId to the base constructor
	{
		User = user;
	}
}