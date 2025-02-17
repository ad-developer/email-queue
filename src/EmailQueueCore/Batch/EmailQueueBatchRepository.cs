using EmailQueueCore.Common;
using EmailQueueCore.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EmailQueueCore.Batch;

public class EmailQueueBatchRepository(IEQContext context, IOptions<EmailQueueOptions> emailQueueOptions) : BaseRepository<EmailQueueBatch>(context), IEmailQueueBatchRepository
{
    public IEnumerable<EmailQueueBatch> GetNextEmailQueueBatchList()
    {
        var options = emailQueueOptions.Value;
        
        return context.EmailQueueBatches
            .Where(p => p.Status == EmailQueueBatchStatus.Queued )
            .Take(options.BatchProcessCount)
            .OrderByDescending(p => p.AddedDate)
            .ToList();
    }

    public IEnumerable<EmailQueueBatch> GetNextEmailQueueBatchFailedList()
    {
        var options = emailQueueOptions.Value;
        
        return context.EmailQueueBatches
            .Where(p => p.Status == EmailQueueBatchStatus.Failed )
            .Take(options.BatchProcessCount)
            .OrderByDescending(p => p.AddedDate)
            .ToList();
    }

    public async Task<IEnumerable<EmailQueueBatch>> GetNextEmailQueueBatchListAsync(CancellationToken cancellationToken = default)
    {
        var options = emailQueueOptions.Value;

        return await context.EmailQueueBatches
            .Where(p => p.Status == EmailQueueBatchStatus.Queued)
            .Take(options.BatchProcessCount)
            .OrderByDescending(p => p.AddedDate)
            .ToListAsync(cancellationToken);
        
    }

    public async Task<IEnumerable<EmailQueueBatch>> GetNextEmailQueueBatchFailedListAsync(CancellationToken cancellationToken = default)
    {
        var options = emailQueueOptions.Value;
        
        return await context.EmailQueueBatches
            .Where(p => p.Status == EmailQueueBatchStatus.Failed )
            .Take(options.BatchProcessCount)
            .OrderByDescending(p => p.AddedDate)
            .ToListAsync(cancellationToken);
    }
}