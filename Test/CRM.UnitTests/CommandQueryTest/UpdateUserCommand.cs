using Crm.Application.Abstraction;

namespace CRM.UnitTests.CommandQueryTest;
public class UpdateUserCommand : ICommand
{
	public int UserId { get; set; }
	public string Name { get; set; }
}
