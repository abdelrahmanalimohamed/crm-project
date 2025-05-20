namespace Crm.Domain.ValueObjects;
public record Email
{
	public string Value { get; }
	private Email() { }
	public Email(string value)
	{
		if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
			throw new ArgumentException("Invalid email format.");
		Value = value;
	}
	public override string ToString() => Value;
}