using Crm.Application.Abstraction;

namespace CRM.UnitTests.CommandQueryTest;
public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, int>
{
	public async ValueTask<int> Handle(CreateUserCommand command, CancellationToken cancellationToken)
	{
		await Task.Delay(10, cancellationToken); // Simulate async work
		return new Random().Next(1, 1000); // Return mock user ID
	}
}
