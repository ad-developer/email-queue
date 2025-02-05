namespace EmailQueueCore.Batch;

public interface IEQBatchHandler
{
    void ScheduleBatches();
    Task ScheduleBatchesAsync(CancellationToken cancellationToken  = default);
    void ProcessBatches();
    Task ProcessBatchesAsync();
}
