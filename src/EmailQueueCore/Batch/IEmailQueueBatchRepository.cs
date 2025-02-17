using EmailQueueCore.Common;

namespace EmailQueueCore.Batch;

public interface IEmailQueueBatchRepository : IBaseRepository<EmailQueueBatch>
{
    IEnumerable<EmailQueueBatch> GetNextEmailQueueBatchList();
    IEnumerable<EmailQueueBatch> GetNextEmailQueueBatchFailedList();
    Task<IEnumerable<EmailQueueBatch>> GetNextEmailQueueBatchListAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<EmailQueueBatch>> GetNextEmailQueueBatchFailedListAsync(CancellationToken cancellationToken = default);
}

