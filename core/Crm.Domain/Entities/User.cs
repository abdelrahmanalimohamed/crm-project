namespace Crm.Domain.Entities;
public class User : BaseEntity
{
	// Private backing fields
	private readonly List<Lead> _leads = new();
	private readonly List<Contact> _contacts = new();
	private readonly List<TaskItem> _tasks = new();
	private readonly List<Note> _notes = new();
	private readonly List<Activity> _activities = new();

	// Properties
	public string Name { get; private set; }
	public Email Email { get; private set; }
	public string PasswordHash { get; private set; }
	public Role Role { get; private set; }
	public IReadOnlyCollection<Lead> Leads => _leads.AsReadOnly();
	public IReadOnlyCollection<Contact> Contacts => _contacts.AsReadOnly();
	public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();
	public IReadOnlyCollection<Note> Notes => _notes.AsReadOnly();
	public IReadOnlyCollection<Activity> Activities => _activities.AsReadOnly();
	private User() { }
	// Constructor for creation
	public User(string name, Email email, string passwordHash, Role role)
	{
		if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
		if (string.IsNullOrWhiteSpace(passwordHash)) throw new ArgumentException("Password is required.");

		Name = name;
		Email = email ?? throw new ArgumentNullException(nameof(email));
		PasswordHash = passwordHash;
		Role = role;
	}
}