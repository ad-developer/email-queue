using EmailQueueCore.Common;
using EmailQueueCore.Configuration;
using Microsoft.Extensions.Options;

namespace EmailQueueCore.Batch;

public class EmailQueueBatchRepository(IEQContext context, IOptions<EmailQueueOptions> emailQueueOptions) : BaseRepository<EmailQueueBatch>(context), IEmailQueueBatchRepository
{
    public IEnumerable<EmailQueueBatch> GetNextEmailQueueBatchList()
    {
        var options = emailQueueOptions.Value;
        
        return context.EmailQueueBatches
            .Where(p => p.Status == EmailQueueBatchStatus.Queued)
            .OrderByDescending(p => p.AddedDate)
            .ToList();
    }
}
