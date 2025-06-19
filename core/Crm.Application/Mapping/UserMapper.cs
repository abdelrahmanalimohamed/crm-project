namespace Crm.Application.Mapping;
public static class UserMapper
{
	public static User MapToUser(CreateUserCommand command, IPasswordHasher passwordHasher)
	{
		var fullName = new FullName(command.FirstName, command.LastName);
		var email = new Email(command.Email);
		string salt;

		var passwordHashed = passwordHasher.HashPassword(command.Password, out salt);

		return new User(fullName, email, passwordHashed, salt , command.Role);
	}
}