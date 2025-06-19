namespace Crm.Application.Employee;
public sealed record CreateUserCommand(
	string FirstName,
	string LastName,
	string Email,
	string Password , 
	Role Role) : ICommand<Guid>;