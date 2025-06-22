namespace Crm.Application.Employee.EmployeeLogin;
public class LoginEmployeeQuery : IQuery<EmployeeLoginResponseDTO>
{
	public EmployeeLoginRequestDTO  employeeLoginRequestDTO { get; set; }
}
