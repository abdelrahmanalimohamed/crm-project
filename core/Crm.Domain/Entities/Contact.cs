namespace Crm.Domain.Entities;
public class Contact : BaseEntity
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public Email Email { get; set; }
	public PhoneNumber Phone { get; set; }

	// Foreign keys
	public Guid CompanyId { get; set; }
	public Guid UserId { get; set; }

	// Navigation properties
	public Company Company { get; set; }
	public User User { get; set; }
	public ICollection<Deal> Deals { get; set; } = new List<Deal>();
	public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
	public ICollection<Note> Notes { get; set; } = new List<Note>();
	public ICollection<Activity> Activities { get; set; } = new List<Activity>();
}