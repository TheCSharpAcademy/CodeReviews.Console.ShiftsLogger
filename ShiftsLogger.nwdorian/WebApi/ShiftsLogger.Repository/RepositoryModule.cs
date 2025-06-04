using Autofac;

namespace ShiftsLogger.Repository;
public class RepositoryModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterType<UserRepository>().AsImplementedInterfaces().InstancePerDependency();
		builder.RegisterType<ShiftRepository>().AsImplementedInterfaces().InstancePerDependency();
		builder.RegisterType<SeedingRepository>().AsImplementedInterfaces().InstancePerDependency();
	}
}
