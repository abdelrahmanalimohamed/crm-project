using Crm.Application.Abstraction;
using Crm.Application.DTO;
using Crm.Application.Employee.DeleteEmployee;
using Crm.Application.Employee.EmployeeCreation;
using Crm.Application.Employee.EmployeeLogin;
using Microsoft.AspNetCore.Mvc;

namespace Crm.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		private readonly ISenderAsync _senderAsync;
		public TestController(ISenderAsync senderAsync)
		{
			_senderAsync = senderAsync;
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser(
			[FromBody] CreateUserCommand command,
			CancellationToken cancellationToken)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var userId = await _senderAsync.Send<CreateUserCommand, Guid>(command, cancellationToken);

			return Ok(userId);
		}

		[HttpPost("login")]
		public async Task<IActionResult> LoginEmployee(
		[FromBody] LoginEmployeeQuery query,
		CancellationToken cancellationToken)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var user = await _senderAsync.SendQuery<LoginEmployeeQuery, EmployeeLoginResponseDTO>(query, cancellationToken);

			if (user == null)
			{
				return Unauthorized(new { message = "Invalid username or password." });
			}

			return Ok(user);
		}

		[HttpDelete("delete")]
		public async Task<IActionResult> DeleteEmployee(
		[FromBody] DeleteEmployeeCommand command,
		CancellationToken cancellationToken)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			await _senderAsync.Send(command, cancellationToken);

			return Ok(new { message = "Employee deleted successfully." });
		}
	}
}
