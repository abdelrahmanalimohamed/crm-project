using Crm.Infrastructure.Services;

namespace Crm.API;
public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddAuthorization();

		builder.Services
			  .AddInfrastructure(builder.Configuration);

		var app = builder.Build();

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.Run();
	}
}
