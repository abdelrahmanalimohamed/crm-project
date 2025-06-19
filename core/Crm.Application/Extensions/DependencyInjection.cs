namespace Crm.Application.Extensions;
public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		var assembly = AppDomain.CurrentDomain.GetAssemblies();

		services.AddScoped<ICustomLogger, MongoDbLogger>();

		services.AddSingleton<ILogEntryFactory, LogEntryFactory>();

		services.AddScoped<IMongoDatabase>(sp =>
		{
			var client = new MongoClient("mongodb://localhost:27017");
			return client.GetDatabase("AppLogs");
		});

		services.Scan(scan => scan
			.FromAssemblies(assembly)
			.AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
				.AsImplementedInterfaces()
				.WithTransientLifetime()
			.AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
				.AsImplementedInterfaces()
				.WithTransientLifetime()
			.AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
				.AsImplementedInterfaces()
				.WithTransientLifetime()
		);

		//services.Decorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
		services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingResultCommandHandlerDecorator<,>));
		//services.Decorate(typeof(IQueryHandler<,>), typeof(LogginQueryHandlerDecorator<,>));

		return services;
	}
}