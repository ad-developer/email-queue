using EmailQueueCore.Common;

namespace EmailQueueCore.Batch;

public interface IEmailQueueBatchRepository : IBaseRepository<EmailQueueBatch>
{
    IEnumerable<EmailQueueBatch> GetNextEmailQueueBatchList();
    IEnumerable<EmailQueueBatch> GetNextEmailQueueBatchFailedList();
}

