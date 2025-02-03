using EmailQueue.Handlers;

namespace EmailQueue;

public class EmailService : IEmailService
{
    private readonly IQueueHandler _queueHandler;

    public EmailService(IQueueHandler queueHandler){
        _queueHandler = queueHandler;
    }

    /// <summary>
    /// <see cref="EmailQueue.IEmailService.AddEmailToQueue(string, string, string, string, string, int, bool)"/>
    /// </summary>
    public void AddEmailToQueue(string emailFrom, string emailTo, string emailTitle, string emailBody, string emailDescription, Guid reffNumber, bool isHtmlBody = true){
        ArgumentException.ThrowIfNullOrWhiteSpace(emailFrom);
        ArgumentException.ThrowIfNullOrWhiteSpace(emailTo);

        _queueHandler.AddToQueue(emailFrom, emailTo, emailTitle, emailBody, emailDescription, reffNumber, isHtmlBody);
    }

    /// <summary>
    /// <see cref="EmailQueue.IEmailService.AddEmailToQueue(string, string, string, string, string, int, bool)"/>
    /// </summary>
    public void AddEmailToQueue(string emailFrom, string emailTo, string emailTitle, string emailBody, string emailDescription, Guid reffNumber){
        AddEmailToQueue(emailFrom, emailTo, emailTitle, emailBody, emailDescription, reffNumber);
    }
}