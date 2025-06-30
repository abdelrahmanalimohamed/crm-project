namespace Crm.Application.Employee.EmployeeLogin;
public sealed class LoginEmployeeHandler :
	IQueryHandler<LoginEmployeeQuery, EmployeeLoginResponseDTO>
{
	private readonly IPasswordHasher _passwordHasher;
	private readonly IDomainDispatcher _domainDispatcher;
	private readonly IBaseRepository<User> _userRepository;
	public LoginEmployeeHandler(
		IPasswordHasher passwordHasher,
		IDomainDispatcher domainDispatcher,
		IBaseRepository<User> userRepository)
	{
		_passwordHasher = passwordHasher;
		_domainDispatcher = domainDispatcher;
		_userRepository = userRepository;
	}

	public async ValueTask<EmployeeLoginResponseDTO?> Handle(
		LoginEmployeeQuery query, 
		CancellationToken cancellationToken)
	{

		var user = await _userRepository.GetFirstOrDefault(
			u => u.Email.Value == query.employeeLoginRequestDTO.email
			, cancellationToken);

		if (user == null ||
			!_passwordHasher.VerifyPassword(
				query.employeeLoginRequestDTO.password, user.Salt, user.PasswordHash))
			return new EmployeeLoginResponseDTO(string.Empty, Guid.Empty);

		await _domainDispatcher.DispatchEvents(user.DomainEvents, cancellationToken);
		user.ClearDomainEvents();

		return new EmployeeLoginResponseDTO(user.Email.Value, user.Id);
	}
}