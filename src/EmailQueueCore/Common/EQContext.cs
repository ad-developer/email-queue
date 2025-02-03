using Microsoft.EntityFrameworkCore;

namespace EmailQueueCore;

public class EQContext : DbContext, IEQContext
{
    public EQContext(DbContextOptions<EQContext> options) : base(options)
    {
    }
    public DbSet<EmailQueue> EmailQueues { get; set; }
    public DbSet<EmailQueueLog> EmailQueueLogs { get; set; }
}