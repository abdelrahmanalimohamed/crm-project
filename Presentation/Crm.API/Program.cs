namespace Crm.API;
public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddControllers();
		// Add services to the container.
		builder.Services.AddAuthorization();

		builder.Services
			  .AddApplication()
			  .AddInfrastructure(builder.Configuration);

		builder.Services.AddHttpContextAccessor();

		var app = builder.Build();

		app.UseHttpsRedirection();

		app.UseAuthorization();
		app.MapControllers();

		app.UseMiddleware<ExceptionHandling>();
		app.Run();
	}
}