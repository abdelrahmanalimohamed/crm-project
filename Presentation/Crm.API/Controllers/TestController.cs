using Crm.Application.Abstraction;
using Crm.Application.DTO;
using Crm.Application.Employee.EmployeeCreation;
using Crm.Application.Employee.EmployeeLogin;
using Microsoft.AspNetCore.Mvc;

namespace Crm.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{

		private readonly ICommandDispatcher _commandDispatcher;
		private readonly IQueryDispatcher _queryDispatcher;
		public TestController(
			ICommandDispatcher commandDispatcher , 
			IQueryDispatcher queryDispatcher)
		{
			_commandDispatcher = commandDispatcher;
			_queryDispatcher = queryDispatcher;
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser(
			[FromBody] CreateUserCommand command, 
			CancellationToken cancellationToken)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var userId = await _commandDispatcher.SendAsync<CreateUserCommand, Guid>(command, cancellationToken);

			return Ok(userId);
		}

		[HttpPost("login")]
		public async Task<IActionResult> LoginEmployee(
		[FromBody] LoginEmployeeQuery query,
		CancellationToken cancellationToken)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var user = await _queryDispatcher.QueryAsync<LoginEmployeeQuery, EmployeeLoginResponseDTO>(query, cancellationToken);

			if (user == null)
			{
				return Unauthorized(new { message = "Invalid username or password." });
			}
			
			return Ok(user);
		}
	}
}
