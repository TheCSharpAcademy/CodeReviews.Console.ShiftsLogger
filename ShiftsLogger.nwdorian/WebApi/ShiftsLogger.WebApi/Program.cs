using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using ShiftsLogger.Root;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
	containerBuilder.RegisterInstance(builder.Configuration).As<IConfiguration>();
	containerBuilder.RegisterAutoMapper(typeof(Program).Assembly);
	containerBuilder.RegisterModule<RootModule>();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(o =>
{
	o.AddOperationTransformer((operation, context, cancellationToken) =>
	{
		operation.OperationId = $"{context.Description.ActionDescriptor.RouteValues.Values.First()}";
		return Task.CompletedTask;
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
