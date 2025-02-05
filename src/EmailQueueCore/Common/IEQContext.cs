using EmailQueueCore.Batch;
using EmailQueueCore.Common;
using Microsoft.EntityFrameworkCore;

namespace EmailQueueCore;

public interface IEQContext : IContext
{
    public DbSet<EmailQueue> EmailQueues { get; set; }
    public DbSet<EmailQueueLog> EmailQueueLogs { get; set; }
    public DbSet<EmailQueueBatch> EmailQueueBatches { get; set; }
}
