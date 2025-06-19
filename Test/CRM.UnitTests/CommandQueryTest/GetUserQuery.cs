using Crm.Application.Abstraction;

namespace CRM.UnitTests.CommandQueryTest;
public class GetUserQuery : IQuery<User>
{
	public int UserId { get; set; }
}
