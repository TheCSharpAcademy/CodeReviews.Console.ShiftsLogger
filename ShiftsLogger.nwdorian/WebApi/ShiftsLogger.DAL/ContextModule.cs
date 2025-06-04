using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ShiftsLogger.DAL;
public class ContextModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.Register(c =>
		{
			var configuration = c.Resolve<IConfiguration>();
			var optionsBuilder = new DbContextOptionsBuilder<ShiftsContext>();
			optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));
			optionsBuilder.EnableSensitiveDataLogging();
			return new ShiftsContext(optionsBuilder.Options);
		})
				.AsSelf()
				.InstancePerLifetimeScope();

		builder.RegisterType<DbInitialiser>().AsSelf().InstancePerDependency();

		builder.RegisterBuildCallback(async container =>
		{
			using var scope = container.BeginLifetimeScope();
			var initialiser = scope.Resolve<DbInitialiser>();
			await initialiser.RunAsync();

		});
	}
}
