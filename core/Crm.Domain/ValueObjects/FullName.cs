namespace Crm.Domain.ValueObjects;
public record FullName
{
	public string FirstName { get; init; }
	public string LastName { get; init; }

	public FullName(string firstName, string lastName)
	{
		if (string.IsNullOrWhiteSpace(firstName))
		{
			throw new ArgumentException("First name cannot be null or whitespace.", nameof(firstName));
		}
		if (string.IsNullOrWhiteSpace(lastName))
		{
			throw new ArgumentException("Last name cannot be null or whitespace.", nameof(lastName));
		}

		FirstName = firstName;
		LastName = lastName;
	}
}