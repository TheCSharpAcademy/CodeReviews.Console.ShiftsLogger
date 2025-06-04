using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using ShiftsLogger.DAL;
using ShiftsLogger.Repository;
using ShiftsLogger.Repository.Profiles;
using ShiftsLogger.Service;

namespace ShiftsLogger.Root;
public class RootModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterAutoMapper(typeof(UserProfile).Assembly);
		builder.RegisterModule<ContextModule>();
		builder.RegisterModule<RepositoryModule>();
		builder.RegisterModule<ServiceModule>();
	}
}
