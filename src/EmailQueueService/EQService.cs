using EmailQueueCore;
using EmailQueueCore.Log;

namespace emailqueueservice;

public class EQService : IEQService
{
    private readonly IEQHandler _queueHandler;
    private readonly IEQLogHandler _logHandler;
      
    public EQService(IEQHandler queueHandler, IEQLogHandler logHandler)
    {
        _queueHandler = queueHandler;
        _logHandler = logHandler;
    }
    
    public Guid AddEmailToQueue(string emailFrom, string emailTo, string emailTitle, string emailBody, string emailDescription, string reffNumber, string requestor, bool isHtmlBody = true)
    {
        return _queueHandler.AddToQueue(emailFrom, emailTo, emailTitle, emailBody, emailDescription, reffNumber, requestor, isHtmlBody);
    }

    public Guid AddEmailToQueue(string emailFrom, string emailTo, string emailTitle, string emailBody, string emailDescription, string reffNumber, string requestor)
    {
       return _queueHandler.AddToQueue(emailFrom, emailTo, emailTitle, emailBody, emailDescription, reffNumber, requestor);
    }
    
    public async Task<Guid> AddEmailToQueueAsync(string emailFrom, string emailTo, string emailTitle, string emailBody, string emailDescription, string reffNumber, string requestor, bool isHtmlBody = true, CancellationToken cancellationToken = default)
    {
        return await _queueHandler.AddToQueueAsync(emailFrom, emailTo, emailTitle, emailBody, emailDescription, reffNumber, requestor, isHtmlBody, cancellationToken);
    }
    
    public async Task<Guid> AddEmailToQueueAsync(string emailFrom, string emailTo, string emailTitle, string emailBody, string emailDescription, string reffNumber, string requestor, CancellationToken cancellationToken = default)
    {
        return await _queueHandler.AddToQueueAsync(emailFrom, emailTo, emailTitle, emailBody, emailDescription, reffNumber, requestor, isHtmlBody: true, cancellationToken);
    }

    public IEnumerable<EmailQueueLog> GetEmailQueueLogById(Guid emailQueueId)
    {
        return _logHandler.GetLogByQueueId(emailQueueId);
    }

    public async Task<IEnumerable<EmailQueueLog>> GetEmailQueueLogByIdAsync(Guid emailQueueId)
    {
        return await _logHandler.GetLogByQueueIdAsync(emailQueueId);    
    }
}
