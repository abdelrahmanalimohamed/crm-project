namespace Crm.Domain.Entities;
public class Lead : BaseEntity
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public Email Email { get; set; }
	public PhoneNumber Phone { get; set; }
	// Foreign key
	public Guid UserId { get; set; }

	// Navigation properties
	public User User { get; set; }
	public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
	public ICollection<Note> Notes { get; set; } = new List<Note>();
	public ICollection<Activity> Activities { get; set; } = new List<Activity>();
}