namespace Crm.Infrastructure.Services;
public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure
		(this IServiceCollection services , 
		IConfiguration configuration)
	{
		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

		return services;
	}
}