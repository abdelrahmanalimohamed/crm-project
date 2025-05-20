using System.ComponentModel.DataAnnotations;

namespace Crm.Domain.Base;
public abstract class BaseEntity
{
	[Key]
	public Guid Id { get; protected set; }
	protected BaseEntity() => Id = Guid.NewGuid();
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
