namespace EmailQueueCore.Log;

public interface IEQLogHandler
{
    IEnumerable<EmailQueueLog> GetLogByQueueId(Guid queueId);
    Task<IEnumerable<EmailQueueLog>> GetLogByQueueIdAsync(Guid queueId);
}
