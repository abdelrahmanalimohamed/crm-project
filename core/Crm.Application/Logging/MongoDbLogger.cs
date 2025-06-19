namespace Crm.Application.Logging;
public class MongoDbLogger : ICustomLogger
{
	private readonly IMongoCollection<LogEntry> _collection;
	public MongoDbLogger(IMongoDatabase database)
	{
		_collection = database.GetCollection<LogEntry>("AppLogs");
	}
	public async Task LogAsync(LogEntry entry, CancellationToken cancellationToken = default)
	{
		await _collection.InsertOneAsync(entry, cancellationToken: cancellationToken);
	}
}