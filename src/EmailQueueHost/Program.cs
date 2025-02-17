using EmailQueueWorker;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args);

host.ConfigureQueueHostedServices(options=>options.ConnectionName = "test" );

var app =  host.Build();

await app.StartAsync();
await app.WaitForShutdownAsync();