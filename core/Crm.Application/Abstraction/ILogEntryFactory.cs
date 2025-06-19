namespace Crm.Application.Abstraction;
public interface ILogEntryFactory
{
	LogEntry CreateLogEntry(
		string level, 
		string message, 
		Exception? exception, 
		Type sourceType);
}