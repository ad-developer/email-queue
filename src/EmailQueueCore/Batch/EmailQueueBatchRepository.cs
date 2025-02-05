using EmailQueueCore.Common;

namespace EmailQueueCore.Batch;

public class EmailQueueBatchRepository(IEQContext context) : BaseRepository<EmailQueueBatch>(context), IEmailQueueBatchRepository
{
}
