namespace EmailQueueCore;

public interface IEQHandler
{
    Guid AddToQueue(string emailFrom, string emailTo, string emailTitle, string emailBody, string emailDescription, string refNumber, string requestor, bool isHtmlBody = true);
    Task<Guid> AddToQueueAsync(string emailFrom, string emailTo, string emailTitle, string emailBody, string emailDescription, string refNumber, string requestor, bool isHtmlBody = true, CancellationToken cancellationToken = default);
    EmailQueue GetFromQueue(Guid emailId);
    Task<EmailQueue> GetFromQueueAsync(Guid emailId);
}
