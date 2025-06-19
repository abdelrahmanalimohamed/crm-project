namespace Crm.Application.Abstraction;

public interface ICustomLogger
{
	Task LogAsync(LogEntry entry, CancellationToken cancellationToken = default);
}