namespace Crm.Domain.ValueObjects;
public record PhoneNumber
{
	public string Value { get; }

	private PhoneNumber() { }
	public PhoneNumber(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
			throw new ArgumentException("Phone number cannot be empty.");
		Value = value;
	}
	public override string ToString() => Value;
}