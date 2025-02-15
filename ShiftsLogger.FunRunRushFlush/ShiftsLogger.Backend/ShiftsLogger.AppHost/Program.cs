var builder = DistributedApplication.CreateBuilder(args);

var backend = builder.AddProject<Projects.ShiftsLogger_Backend>("Backend");

builder.AddProject<Projects.ShiftsLogger_Frontend>("Frontend")
.WithExternalHttpEndpoints()
    .WithReference(backend)
    .WaitFor(backend);

builder.Build().Run();

