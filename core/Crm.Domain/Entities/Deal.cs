namespace Crm.Domain.Entities;
public class Deal : BaseEntity
{
	public string Title { get; set; }
	public decimal Amount { get; set; }
	// Foreign keys
	public Guid ContactId { get; set; }
	public Guid CompanyId { get; set; }
	// Navigation properties
	public Contact Contact { get; set; }
	public Company Company { get; set; }
	public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
	public ICollection<Note> Notes { get; set; } = new List<Note>();
	public ICollection<Activity> Activities { get; set; } = new List<Activity>();
}