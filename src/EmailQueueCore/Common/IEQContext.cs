using EmailQueueCore.Common;
using Microsoft.EntityFrameworkCore;

namespace EmailQueueCore;

public interface IEQContext : IContext
{
    public DbSet<EmailQueue> EmailQueues { get; set; }
}
