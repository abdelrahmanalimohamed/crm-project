namespace Crm.Application.Employee.DeleteEmployee;
public class DeleteEmployeeHandler : ICommandHandler<DeleteEmployeeCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IBaseRepository<User> _userRepository;
	public DeleteEmployeeHandler(
		IUnitOfWork unitOfWork, IBaseRepository<User> userRepository)
	{
		_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		_userRepository= userRepository;
	}
	public async ValueTask Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
	{
		var emp = await _userRepository.GetFirstOrDefault(x => x.Id == command.employeeId , cancellationToken);

		if (emp == null)
		{
			throw new Exception($"Employee with ID {command.employeeId} not found.");
		}

		await _userRepository.Delete(emp, cancellationToken);

		await _unitOfWork.CommitAsync(cancellationToken);
	}
}