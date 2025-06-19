using Crm.Application.Abstraction;
using Crm.Application.Employee;
using Microsoft.AspNetCore.Mvc;

namespace Crm.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{

		private readonly ICommandDispatcher _commandDispatcher;
		public TestController(ICommandDispatcher commandDispatcher)
		{
			_commandDispatcher = commandDispatcher;
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

	}
}
