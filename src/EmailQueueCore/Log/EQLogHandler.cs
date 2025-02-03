namespace EmailQueueCore.Log;

public class EQLogHandler : IEQLogHandler
{
    private readonly IEmailQueueLogRepository _emailQueueLogRepository;

    public EQLogHandler(IEmailQueueLogRepository emailQueueLogRepository)
    {
        _emailQueueLogRepository = emailQueueLogRepository;
    }

    public IEnumerable<EmailQueueLog> GetLogByQueueId(Guid queueId)
    {
        return _emailQueueLogRepository.Get(p=>p.Id == queueId);
    }
    public async Task<IEnumerable<EmailQueueLog>> GetLogByQueueIdAsync(Guid queueId)
    {
        return await _emailQueueLogRepository.GetAsync(p=>p.Id == queueId);
    }
}
