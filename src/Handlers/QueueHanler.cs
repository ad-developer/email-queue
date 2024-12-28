namespace EmailQueue.Handlers;

public class QueueHanler : IQueueHandler
{
    
    private readonly IEmailServiceDbContext _dbContext;

    public QueueHanler(IEmailServiceDbContext dbContext){
        _dbContext = dbContext;
    }

    
    public void AddToQueue(string emailFrom, string emailTo, string emailTitle, string emailBody, string emailDescription, Guid refNumber, bool isHtmlBody = true){

        var emailQueue = new Entities.EmailQueue(emailTo, emailFrom, emailTitle, emailBody, isHtmlBody, emailDescription, refNumber);
        _dbContext.EmailQueues.Add(emailQueue);
        _dbContext.SaveChanges();
    }
}
