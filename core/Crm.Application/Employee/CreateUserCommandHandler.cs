namespace Crm.Application.Employee;
public sealed class CreateUserCommandHandler
	(IUnitOfWork unitOfWork , 
	 IDomainDispatcher domainDispatcher ,
	 IPasswordHasher passwordHasher) 
	: ICommandHandler<CreateUserCommand, Guid>
{
	public async ValueTask<Guid> Handle(
		CreateUserCommand command,
		CancellationToken cancellationToken)
	{
		var user = UserMapper.MapToUser(command, passwordHasher);

		await unitOfWork.Repository<User>().Insert(user, cancellationToken);
		await unitOfWork.SaveChangesAsync(cancellationToken);

		await domainDispatcher.DispatchEvents(user.DomainEvents, cancellationToken);
		user.ClearDomainEvents();

		return user.Id;
	}
}