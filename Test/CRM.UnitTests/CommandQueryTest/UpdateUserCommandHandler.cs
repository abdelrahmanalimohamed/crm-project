using Crm.Application.Abstraction;

namespace CRM.UnitTests.CommandQueryTest;
public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
	public async ValueTask Handle(UpdateUserCommand command, CancellationToken cancellationToken)
	{
		await Task.Delay(10, cancellationToken);				
	}
}