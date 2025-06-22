namespace Crm.Application.Employee.EmployeeCreation;
public sealed record CreateUserCommand(
	string FirstName,
	string LastName,
	string Email,
	string Password , 
	Role Role) : ICommand<Guid>;