namespace Crm.Application.Employee.EmployeeCreation;
public sealed class CreateUserCommandHandler
	(IUnitOfWork unitOfWork , 
	 IDomainDispatcher domainDispatcher ,
	 IBaseRepository<User> userRepository,
	 IPasswordHasher passwordHasher) 
	: ICommandHandler<CreateUserCommand, Guid>
{
	public async ValueTask<Guid> Handle(
		CreateUserCommand command,
		CancellationToken cancellationToken)
	{
		var user = UserMapper.MapToUser(command, passwordHasher);

		await userRepository.Insert(user, cancellationToken);
		await unitOfWork.CommitAsync(cancellationToken);

		await domainDispatcher.DispatchEvents(user.DomainEvents, cancellationToken);
		user.ClearDomainEvents();

		return user.Id;
	}
}