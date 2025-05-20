namespace Crm.Domain.Entities;
public class Company : BaseEntity
{
	public string Name { get; set; }
	public string Industry { get; set; }
	public Address Address { get; set; }
	public Email Email { get; set; }
	public PhoneNumber PhoneNumber { get; set; }
	public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
	public ICollection<Deal> Deals { get; set; } = new List<Deal>();
	public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
	public ICollection<Note> Notes { get; set; } = new List<Note>();
	public ICollection<Activity> Activities { get; set; } = new List<Activity>();
}