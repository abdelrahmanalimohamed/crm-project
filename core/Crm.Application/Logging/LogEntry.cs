namespace Crm.Application.Logging;

public class LogEntry
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string? Id { get; set; }

	public DateTime Timestamp { get; set; }
	public string Level { get; set; } = string.Empty;
	public string Message { get; set; } = string.Empty;
	public string SourceContext { get; set; } = string.Empty;
	public string? Exception { get; set; }
	public BsonDocument?  Properties { get; set; }
}
