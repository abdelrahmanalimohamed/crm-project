namespace Crm.Infrastructure.Security;
public class PasswordHasher : IPasswordHasher
{
	public string HashPassword(string password, out string salt)
	{
		var saltBytes = GenerateSalt();
		salt = Convert.ToBase64String(saltBytes);

		var passwordBytes = Encoding.UTF8.GetBytes(password);

		var argonConfig = ArgonConfig(passwordBytes, saltBytes);

		var hashBytes = argonConfig.GetBytes(32);
		return Convert.ToBase64String(hashBytes);
	}
	public bool VerifyPassword(string password, string salt, string hashedPassword)
	{
		var saltBytes = Convert.FromBase64String(salt);
		var passwordBytes = Encoding.UTF8.GetBytes(password);


		var argonConfig = ArgonConfig(passwordBytes, saltBytes);

		var hashBytes = argonConfig.GetBytes(32);
		string calculatedHash = Convert.ToBase64String(hashBytes);

		return calculatedHash == hashedPassword;
	}

	private Argon2id ArgonConfig(byte[] passwordBytes , byte[] saltBytes)
	{
		var argon2 = new Argon2id(passwordBytes)
		{
			Salt = saltBytes,
			DegreeOfParallelism = 4,
			MemorySize = 65536,
			Iterations = 4
		};
		return argon2;
	}
	private byte[] GenerateSalt(int length = 16)
	{
		using var rng = RandomNumberGenerator.Create();
		var salt = new byte[length];
		rng.GetBytes(salt);
		return salt;
	}
}