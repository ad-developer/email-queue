using EmailQueueCore.Common;
using EmailQueueCore.Queue;
using Microsoft.EntityFrameworkCore;

namespace EmailQueueCore;

public class EmailQueueRepository(IEQContext context) : BaseRepository<EmailQueue>(context), IEmailQueueRepository
{
    public EmailQueue GetNextEmailQueue()
    {
        return context.EmailQueues
            .Where(p => p.Status == EmailQueueStatus.Queued)
            .OrderByDescending(p => p.AddedDate)
            .FirstOrDefault()!;
    }
    public async Task<EmailQueue> GetNextEmailQueueAsync()
    {
        return (await context.EmailQueues
            .Where(p => p.Status == EmailQueueStatus.Queued)
            .OrderBy(p => p.AddedDate)
            .FirstOrDefaultAsync())!;
    }    
}