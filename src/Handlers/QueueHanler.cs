namespace EmailQueue.Handlers;

public class QueueHanler : IQueueHandler
{
    

    public QueueHanler(){

    }

    public void AddToQueue(string emailFrom, string emailTo, string emailTitle, string emailBody, string emailDescription, Guid refNumber, bool isHtmlBody = true){

    }
}
