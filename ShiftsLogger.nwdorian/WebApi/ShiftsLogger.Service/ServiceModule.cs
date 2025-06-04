using Autofac;

namespace ShiftsLogger.Service;
public class ServiceModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterType<UserService>().AsImplementedInterfaces().InstancePerDependency();
		builder.RegisterType<ShiftService>().AsImplementedInterfaces().InstancePerDependency();
		builder.RegisterType<SeedingService>().AsImplementedInterfaces().InstancePerDependency();
	}
}
