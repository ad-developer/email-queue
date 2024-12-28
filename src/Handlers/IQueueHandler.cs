namespace EmailQueue.Handlers;

public interface IQueueHandler
{
    /// <summary>
    /// AddToQueue
    /// </summary>
    /// <param name="emailFrom"></param>
    /// <param name="emailTo">Can be either single or multiple emails separated by;</param>
    /// <param name="emailTitle"></param>
    /// <param name="emailBody"></param>
    /// <param name="emailDescription"></param>
    /// <param name="refNumber"></param>
    /// <param name="isHtmlBody"></param>
    void AddToQueue(string emailFrom, string emailTo, string emailTitle, string emailBody, string emailDescription, Guid refNumber, bool isHtmlBody = true);
}
