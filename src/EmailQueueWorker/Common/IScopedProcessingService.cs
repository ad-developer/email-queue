namespace EmailQueueWorker.Common;

public interface IScopedProcessingService
{
    Task DoWorkAsync(CancellationToken cancellationToken);
}
