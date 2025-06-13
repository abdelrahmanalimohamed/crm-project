using Crm.Application.Abstraction;

namespace CRM.UnitTests.CommandQueryTest;
public class CreateUserCommand : ICommand<int>
{
	public string Name { get; set; }
	public string Email { get; set; }
}
