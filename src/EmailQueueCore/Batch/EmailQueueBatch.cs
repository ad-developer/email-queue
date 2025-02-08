using System.ComponentModel.DataAnnotations;
using EmailQueueCore.Common;

namespace EmailQueueCore.Batch;

public class EmailQueueBatch: IEntity
{
    public Guid Id { get; set; }
    public Guid EmailQueueId { get; set; }
    

    [Required]
    [EmailAddressList]
    public required string EmailTo { get; set; } 

    [Required]
    [EmailAddress]
    public required string EmailFrom { get; set; }
    public required string EmailTitle { get; set; }
    public required string EmailBody { get; set; }
    public bool IsHtml { get; set; }

    public  string? EmailToFailed { get; set; } 
    
    public int BatchNumber { get; set; }
    public DateTime AddedDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? ProcessStarted { get; set; }
    public DateTime? ProcessEnded { get; set; }
    public int ProcessedCount { get; set; } = 0;
    public int FailedCount { get; set; } = 0;
    
    public string? LastFailureMessage { get; set; }

    [Required]
    public  EmailQueueBatchStatus Status { get; set; } = EmailQueueBatchStatus.Queued;
}