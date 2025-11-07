using BackendChallenge.Infrastructure.Configurations;
using BackendChallenge.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext();

var host = builder.Build();
host.Run();
