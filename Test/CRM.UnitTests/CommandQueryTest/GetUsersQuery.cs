using Crm.Application.Abstraction;

namespace CRM.UnitTests.CommandQueryTest;
public class GetUsersQuery : IQuery<List<User>>
{
	public int PageSize { get; set; }
	public int PageNumber { get; set; }
}