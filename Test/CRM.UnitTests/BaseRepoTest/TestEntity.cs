namespace CRM.UnitTests.BaseRepoTest;
internal class TestEntity
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTime CreatedDate { get; set; }
	public bool? IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
}	