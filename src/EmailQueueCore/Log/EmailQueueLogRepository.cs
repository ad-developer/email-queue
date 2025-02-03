using EmailQueueCore.Common;

namespace EmailQueueCore.Log;

public class EmailQueueLogRepository : BaseRepository<EmailQueueLog>, IEmailQueueLogRepository
{
    public EmailQueueLogRepository(IEQContext context) : base(context)
    {
    }
}
