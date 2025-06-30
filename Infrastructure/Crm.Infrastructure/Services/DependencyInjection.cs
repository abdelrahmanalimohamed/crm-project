namespace Crm.Infrastructure.Services;
public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure
		(this IServiceCollection services , 
		IConfiguration configuration)
	{
		services.AddDbContext<DbContext, ApplicationDbContext>(options =>
			options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

		services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
		services.AddScoped<IUnitOfWork, UnitOfWorkImplementation>();

		services.AddScoped<IDomainDispatcher, DomainEventDispatcher>();
		services.AddScoped<IPasswordHasher, PasswordHasher>();
		services.AddScoped<ICommandDispatcher, CommandDispatcher>();
		services.AddScoped<IQueryDispatcher, QueryDispatcher>();
		services.AddScoped<ISenderAsync, SenderAsync>();

		var assembly = typeof(DependencyInjection).Assembly;

		services.AddMediatR(configuration =>
		  configuration.RegisterServicesFromAssembly(assembly));

		return services;
	}
}