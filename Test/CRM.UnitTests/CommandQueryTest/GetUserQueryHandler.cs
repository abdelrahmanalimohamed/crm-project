using Crm.Application.Abstraction;

namespace CRM.UnitTests.CommandQueryTest;
internal class GetUserQueryHandler : IQueryHandler<GetUserQuery, User>
{
	public async ValueTask<User> Handle(GetUserQuery query, CancellationToken cancellationToken)
	{
		await Task.Delay(10, cancellationToken); // Simulate async work
		return new User
		{
			Id = query.UserId,
			Name = "Test User",
			Email = "test@example.com"
		};
	}
}