namespace Crm.Application.Employee.DeleteEmployee;
public record DeleteEmployeeCommand(Guid employeeId) : ICommand;
