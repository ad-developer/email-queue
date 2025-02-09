using System.ComponentModel.DataAnnotations;
using EmailQueueCore.Common;

namespace EmailQueueCore;

public class EmailQueueLog : IEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public required string RefNumber { get; set; }
    [Required]
    public required Guid EmailQueueId { get; set; }
    
    public Guid? EmailQueueBatchId { get; set; }
    public Guid? EmailQueueBatchProcessId  { get; set; }
    public int? EmailQueueBatchNumber { get; set; }
    public required string Action { get; set; }  
    public required string Details { get; set; }

    // Static methods
    public static EmailQueueLog Create(string refNumber, Guid emailQueueId, string action, string details)
    {
        return  Create(refNumber, emailQueueId, null, null, action, details);
    }

    public static EmailQueueLog Create(string refNumber, Guid emailQueueId, Guid? emailQueueBatchId, int? emailQueueBatchNumber, string action, string details)
    {
       return Create(refNumber, emailQueueId, emailQueueBatchId, emailQueueBatchNumber, null, action, details);
    }

     public static EmailQueueLog Create(string refNumber, Guid emailQueueId, Guid? emailQueueBatchId, int? emailQueueBatchNumber, Guid? emailQueueBatchProcessId,string action, string details)
    {
        return new EmailQueueLog
        {
            RefNumber = refNumber,
            EmailQueueId = emailQueueId,
            EmailQueueBatchId = emailQueueBatchId,
            EmailQueueBatchNumber = emailQueueBatchNumber,
            EmailQueueBatchProcessId = emailQueueBatchProcessId,
            Action = action,
            Details = details  
        }; 
    }
}