using Crm.Application.Abstraction;

namespace CRM.UnitTests.CommandQueryTest;
internal class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, List<User>>
{
	public async ValueTask<List<User>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
	{
		await Task.Delay(10, cancellationToken); // Simulate async work
	    return new List<User>
		{
			new User { Id = 1, Name = "User 1", Email = "user1@example.com" },
			new User { Id = 2, Name = "User 2", Email = "user2@example.com" }
		};
	}
}