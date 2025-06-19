namespace Crm.Application.Abstraction;
public interface ICommand
{
}
public interface ICommand<out TResult> : ICommand
{
}