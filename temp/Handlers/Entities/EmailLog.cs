namespace EmailQueue.Handlers.Entities;

public class EmailLog
{
    public int Id { get; set; }
    public Guid RefNumber { get; set; }
    public int EmailQueueId { get; set; }   
    public string Action { get; set; }  
    public string Description { get; set; }
    
}
