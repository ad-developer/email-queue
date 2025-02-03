using System.Dynamic;
using EmailQueueCore;

namespace emailqueueservice;

public interface IEQService
{
    Guid AddEmailToQueue(string emailFrom, string emailTo, string emailTitle, string emailBody, string emailDescription, string reffNumber, string requestor, bool isHtmlBody = true);
    
    Guid AddEmailToQueue(string emailFrom, string emailTo, string emailTitle, string emailBody, string emailDescription, string reffNumber, string requestor);

    Task<Guid> AddEmailToQueueAsync(string emailFrom, string emailTo, string emailTitle, string emailBody, string emailDescription, string reffNumber, string requestor, bool isHtmlBody = true, CancellationToken cancellationToken = default);

    Task<Guid> AddEmailToQueueAsync(string emailFrom, string emailTo, string emailTitle, string emailBody, string emailDescription, string reffNumber, string requestor, CancellationToken cancellationToken = default);

    IEnumerable<EmailQueueLog> GetEmailQueueLogById(Guid emailQueueId);

    Task<IEnumerable<EmailQueueLog>> GetEmailQueueLogByIdAsync(Guid emailQueueId);
}