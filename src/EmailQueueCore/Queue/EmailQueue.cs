using System.ComponentModel.DataAnnotations;
using EmailQueueCore.Common;
using EmailQueueCore.Queue;

namespace EmailQueueCore;

public class EmailQueue(string emailTo, string emailFrom, string emailTitle, string emailBody, bool isHtml, string emailDescription, string refNumber, string addedBy) : IEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [EmailAddressList]
    public string EmailTo { get; set; } = emailTo;

    [Required]
    [EmailAddress]
    public string EmailFrom { get; set; } = emailFrom;

    public string EmailTitle { get; set; } = emailTitle;

    public string EmailBody { get; set; } = emailBody;

    public bool IsHtml { get; set; } = isHtml;

    [Required]
    public string EmailDescription { get; set; } = emailDescription;

    [Required]
    public string RefNumber { get; set; } = refNumber;

    [Required]
    public DateTime AddedDate { get; set; } = DateTime.UtcNow;

    [Required]
    [StringLength(maximumLength: 255, MinimumLength = 3)]
    public string AddedBy { get; set; } = addedBy;

    public DateTime? UpdatedDate { get; set; }
    
    [StringLength(maximumLength: 255, MinimumLength = 3)]
    public string? UpdatedBy { get; set; }

    [Required]
    public  EmailQueueStatus Status { get; set; } = EmailQueueStatus.Queued;
}
