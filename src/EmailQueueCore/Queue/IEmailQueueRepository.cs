using EmailQueueCore.Common;

namespace EmailQueueCore;

public interface IEmailQueueRepository : IBaseRepository<EmailQueue>
{
    EmailQueue GetNextEmailQueue();
    Task<EmailQueue> GetNextEmailQueueAsync();
}
