namespace EmailQueue;

public interface IEmailService
{
    /// <summary>
    /// AddEmailToQueue
    /// </summary>
    /// <param name="emailFrom"></param>
    /// <param name="emailTo"></param>
    /// <param name="emailTitle"></param>
    /// <param name="emailBody"></param>
    /// <param name="emailDescription"></param>
    /// <param name="reffNumber"></param>
    /// <param name="isHtmlBody"></param>
    void AddEmailToQueue(string emailFrom, string emailTo, string emailTitle, string emailBody, string emailDescription, Guid reffNumber, bool isHtmlBody = true);
    
    /// <summary>
    /// AddEmailToQueue
    /// </summary>
    /// <param name="emailFrom"></param>
    /// <param name="emailTo"></param>
    /// <param name="emailTitle"></param>
    /// <param name="emailBody"></param>
    /// <param name="emailDescription"></param>
    /// <param name="reffNumber"></param>
    void AddEmailToQueue(string emailFrom, string emailTo, string emailTitle, string emailBody, string emailDescription, Guid reffNumber);

    /// <summary>
    /// ProcessEmailQueue
    /// </summary>
    void ProcessEmailQueue();
}