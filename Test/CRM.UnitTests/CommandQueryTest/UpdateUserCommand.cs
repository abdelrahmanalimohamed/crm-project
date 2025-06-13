using Crm.Application.Abstraction;

namespace CRM.UnitTests.CommandQueryTest;
internal class UpdateUserCommand : ICommand
{
	public int UserId { get; set; }
	public string Name { get; set; }
}
