namespace Crm.Application.Employee.DeleteEmployee;
public class DeleteEmployeeHandler : ICommandHandler<DeleteEmployeeCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	public DeleteEmployeeHandler(
		IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
	}
	public async ValueTask Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
	{
		var emp = await _unitOfWork.Repository<User>()
			.GetFirstOrDefault(x => x.Id == command.employeeId
		, cancellationToken);

		if (emp == null)
		{
			throw new Exception($"Employee with ID {command.employeeId} not found.");
		}

		await _unitOfWork.Repository<User>().Delete(emp, cancellationToken);

		await _unitOfWork.CommitAsync(cancellationToken);
	}
}