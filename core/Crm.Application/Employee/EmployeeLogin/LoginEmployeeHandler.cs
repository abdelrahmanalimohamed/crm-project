namespace Crm.Application.Employee.EmployeeLogin;
public sealed class LoginEmployeeHandler :
	IQueryHandler<LoginEmployeeQuery, EmployeeLoginResponseDTO>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IPasswordHasher _passwordHasher;
	private readonly IDomainDispatcher _domainDispatcher;
	public LoginEmployeeHandler(
		IUnitOfWork unitOfWork,
		IPasswordHasher passwordHasher,
		IDomainDispatcher domainDispatcher)
	{
		_unitOfWork = unitOfWork;
		_passwordHasher = passwordHasher;
		_domainDispatcher = domainDispatcher;
	}

	public async ValueTask<EmployeeLoginResponseDTO?> Handle(
		LoginEmployeeQuery query, 
		CancellationToken cancellationToken)
	{

		var user = await _unitOfWork.Repository<User>().GetFirstOrDefault(
			u => u.Email.Value == query.employeeLoginRequestDTO.email
			, cancellationToken);

		if (user == null ||
			!_passwordHasher.VerifyPassword(
				query.employeeLoginRequestDTO.password, user.Salt, user.PasswordHash))
			return null;

		await _domainDispatcher.DispatchEvents(user.DomainEvents, cancellationToken);
		user.ClearDomainEvents();

		return new EmployeeLoginResponseDTO(user.Email.Value, user.Id);
	}
}