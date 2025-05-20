namespace Crm.Domain.ValueObjects;
public record Address
{
	public string Street { get; }
	public string City { get; }
	public string Country { get; }
	private Address() { } 
	public Address(string street, string city, string country)
	{
		Street = street;
		City = city;
		Country = country;
	}
}