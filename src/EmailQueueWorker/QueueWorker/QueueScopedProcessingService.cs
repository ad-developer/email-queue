using EmailQueueCore.Batch;
using EmailQueueCore.Configuration;
using EmailQueueWorker.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmailQueueWorker.QueueWorker;

public class QueueScopedProcessingService(IOptions<EmailQueueOptions> emailQueueOptions, IEQBatchHandler iEQBatchHandler, ILogger<QueueScopedProcessingService> logger) : IScopedProcessingService
{
    private readonly ILogger _logger = logger;
    private readonly EmailQueueOptions _options = emailQueueOptions.Value;
    private readonly IEQBatchHandler _iEQBatchHandler = iEQBatchHandler;

    public async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        Guid cycleId = Guid.NewGuid();
        long cycleCount = 0;
        
        _logger.LogInformation($"QueueScopedProcessingService started, id {cycleId}"); 
        while (!cancellationToken.IsCancellationRequested)
        {
             cycleCount++;
            _logger.LogInformation($"QueueScopedProcessingService started new cycle number {cycleCount}, id number {cycleId}"); 
            await _iEQBatchHandler.ScheduleBatchesAsync(cancellationToken);
            
            var delay = _options.QueueProcessingWorkerCycleDelay * 1000;
            await Task.Delay(delay, cancellationToken);
        }
    }
}