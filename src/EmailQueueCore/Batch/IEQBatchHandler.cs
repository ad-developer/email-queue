namespace EmailQueueCore.Batch;

public interface IEQBatchHandler
{
    void ScheduleBatches();
    Task ScheduleBatchesAsync(CancellationToken cancellationToken);
    void ProcessBatches();
    Task ProcessBatchesAsync(CancellationToken cancellationToken);
}
