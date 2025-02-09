using System.ComponentModel.DataAnnotations;
using EmailQueueCore.Common;

namespace EmailQueueCore.Batch;

public class EmailQueueBatch: IEntity
{
    public Guid Id { get; set; }
    public Guid EmailQueueId { get; set; }
    public string RefNumber { get; set; }
    
    // Email related fields
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

    // Batch related fields
    public int BatchNumber { get; set; }
    public DateTime AddedDate { get; set; } = DateTime.UtcNow;
    [Required]
    public  EmailQueueBatchStatus Status { get; set; } = EmailQueueBatchStatus.Queued;

    // Process related fields
    public DateTime? ProcessStarted { get; set; }
    public DateTime? ProcessEnded { get; set; }
    public Guid? LastBatchProcessId { get; set; }
    public int ProcessedCount { get; set; } = 0;
    public int FailedCount { get; set; } = 0;
    
    // Failure related field
    public string? LastFailureMessage { get; set; }
    
}