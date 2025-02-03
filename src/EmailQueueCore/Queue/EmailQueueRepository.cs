using EmailQueueCore.Common;

namespace EmailQueueCore;

public class EmailQueueRepository(IEQContext context) : BaseRepository<EmailQueue>(context), IEmailQueueRepository
{
}

