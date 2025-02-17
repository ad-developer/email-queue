using EmailQueueCore.Batch;
using EmailQueueCore.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmailQueueWorker.QueueWorker;

public class QueueProcessingHostedService(IServiceProvider services, IOptions<EmailQueueOptions> options, ILogger<QueueProcessingHostedService> logger) : BackgroundService
{
    private readonly ILogger<QueueProcessingHostedService> _logger = logger;
    private readonly EmailQueueOptions _options = options.Value;
    public IServiceProvider Services { get; } = services;

    Guid _processId = Guid.NewGuid();

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await DoWorkAsync(cancellationToken);
    }

    internal async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        long cycleCount = 0;
        _logger.LogInformation($"QueueProcessingHostedService started, process id {_processId}, time {DateTime.UtcNow}"); 
        
        while (!cancellationToken.IsCancellationRequested)
        {
            cycleCount++;
            
            using (var scope = Services.CreateScope())
            {
                Guid scopeId = Guid.NewGuid();

                _logger.LogInformation($"QueueProcessingHostedService started new cycle, number {cycleCount}, process id {_processId}, scope id {scopeId}, ime {DateTime.UtcNow}"); 
            
                var handler = 
                    scope.ServiceProvider
                        .GetRequiredService<IEQBatchHandler>();
                await handler.ScheduleBatchesAsync(cancellationToken);
                 
                 _logger.LogInformation($"QueueProcessingHostedService completed new cycle, number {cycleCount}, process id {_processId}, scope id {scopeId}, time {DateTime.UtcNow}"); 
            
            }

            var delay = _options.QueueProcessingWorkerCycleDelay * 1000;
            await Task.Delay(delay, cancellationToken);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"QueueProcessingHostedService is stopping, process id {_processId} time {DateTime.UtcNow}");

        await base.StopAsync(cancellationToken);
    }
}
