namespace Crm.Application.Logging;
public class LogEntryFactory : ILogEntryFactory
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	public LogEntryFactory(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}
	public LogEntry CreateLogEntry(string level, string message, Exception? exception, Type sourceType)
	{
		var context = _httpContextAccessor.HttpContext;
		var headers = context?.Request.Headers.Select(h => new BsonElement(h.Key, h.Value.ToString())) 
			?? Enumerable.Empty<BsonElement>();

		return new LogEntry
		{
			Timestamp = DateTime.UtcNow,
			Level = level,
			Message = message,
			Exception = exception?.ToString(),
			SourceContext = exception?.TargetSite?.DeclaringType?.FullName ?? sourceType.FullName ?? "Unknown",
			Properties = new BsonDocument
			{
				{ "Path", context?.Request.Path.ToString() ?? "N/A" },
				{ "Method", context?.Request.Method ?? "N/A" },
				{ "QueryString", context?.Request.QueryString.ToString() ?? "N/A" },
				{ "Host", context?.Request.Host.ToString() ?? "N/A" },
				{ "Scheme", context?.Request.Scheme ?? "N/A" },
				{ "User", context?.User?.Identity?.Name ?? "Anonymous" },
				{ "Headers", new BsonDocument(headers) }
			}
		};
	}
}
