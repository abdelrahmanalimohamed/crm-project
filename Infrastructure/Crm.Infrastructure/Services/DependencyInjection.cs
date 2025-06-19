namespace Crm.Infrastructure.Services;
public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure
		(this IServiceCollection services , 
		IConfiguration configuration)
	{
		services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
		services.AddScoped<IUnitOfWork, UnitOfWorkImplementation>();
		services.AddScoped<IDomainDispatcher, DomainEventDispatcher>();
		services.AddScoped<IPasswordHasher, PasswordHasher>();
		services.AddScoped<ICommandDispatcher, CommandDispatcher>();

		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

		var assembly = typeof(DependencyInjection).Assembly;

		services.AddMediatR(configuration =>
		  configuration.RegisterServicesFromAssembly(assembly));

		return services;
	}
}