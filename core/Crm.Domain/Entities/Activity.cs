namespace Crm.Domain.Entities;
public class Activity : BaseEntity
{
	public ActivityTypes Type { get; set; }
	public string Description { get; set; }
	public DateTime ActivityDate { get; set; }

	// Foreign keys (nullable for optional relationships)
	public Guid UserId { get; set; }
	public Guid LeadId { get; set; }
	public Guid ContactId { get; set; }
	public Guid CompanyId { get; set; }
	public Guid DealId { get; set; }

	// Navigation properties
	public User User { get; set; }
	public Lead Lead { get; set; }
	public Contact Contact { get; set; }
	public Company Company { get; set; }
	public Deal Deal { get; set; }
}