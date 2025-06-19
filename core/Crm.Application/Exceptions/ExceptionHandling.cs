namespace Crm.Application.Exceptions;
public class ExceptionHandling
{
	private readonly RequestDelegate _next;
	private readonly ILogEntryFactory _logEntryFactory;
	public ExceptionHandling(
		RequestDelegate next ,
		ILogEntryFactory logEntryFactory)
	{
		_next = next;
		_logEntryFactory = logEntryFactory;
	}
	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(context, ex);
		}
	}
	private async Task HandleExceptionAsync(
		HttpContext context, 
		Exception exception , 
		CancellationToken cancellationToken = default)
	{
		context.Response.ContentType = "application/json";

		HttpStatusCode status;
		object response;

		status = HttpStatusCode.InternalServerError;
		response = new { error = exception.Message };

		var logger = context.RequestServices.GetRequiredService<ICustomLogger>(); 

		var logEntry = _logEntryFactory.CreateLogEntry(
			level: "Error",
			message: exception.Message,
			exception: exception,
			sourceType: typeof(ExceptionHandling)
		);

		await logger.LogAsync(logEntry, cancellationToken);

		context.Response.StatusCode = (int)status;
		var jsonResponse = JsonSerializer.Serialize(response);

		await context.Response.WriteAsync(jsonResponse);
	}
}