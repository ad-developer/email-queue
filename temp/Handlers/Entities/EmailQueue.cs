using System.ComponentModel.DataAnnotations;
using EmailQueue.Validation;

namespace EmailQueue.Handlers.Entities;


/// <summary>
/// Represents an email queue entity.
/// </summary>
public class EmailQueue
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailQueue"/> class.
    /// </summary>
    /// <param name="emailTo">The recipient email address.</param>
    /// <param name="emailFrom">The sender email address.</param>
    /// <param name="emailTitle">The title of the email.</param>
    /// <param name="emailBody">The body content of the email.</param>
    /// <param name="isHtml">Indicates whether the email body is in HTML format.</param>
    /// <param name="emailDescription">A description of the email.</param>
    /// <param name="refNumber">A reference number for the email.</param>
    /// <param name="addedBy">The user who added the email to the queue.</param>
    public EmailQueue(string emailTo, string emailFrom, string emailTitle, string emailBody, bool isHtml, string emailDescription, Guid refNumber, string addedBy)
    {
        // Constructor implementation
    }

    /// <summary>
    /// Gets or sets the unique identifier for the email queue entry.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the recipient email address. It can be either a single email or multiple emails separated by a semicolon.
    /// </summary>
    [Required]
    [EmailAddressList]
    public string EmailTo { get; set; }

    /// <summary>
    /// Gets or sets the sender email address.
    /// </summary>
    [Required]
    [EmailAddress]
    public string EmailFrom { get; set; }

    /// <summary>
    /// Gets or sets the title of the email.
    /// </summary>
    public string EmailTitle { get; set; }

    /// <summary>
    /// Gets or sets the body content of the email.
    /// </summary>
    public string EmailBody { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the email body is in HTML format.
    /// </summary>
    public bool IsHtml { get; set; } = false;

    /// <summary>
    /// Gets or sets a description of the email.
    /// </summary>
    public string EmailDescription { get; set; }

    /// <summary>
    /// Gets or sets the reference number for the email.
    /// </summary>
    [Required]
    public Guid RefNumber { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the email was added to the queue.
    /// </summary>
    [Required]
    public DateTime AddedDate { get; set; }

    /// <summary>
    /// Gets or sets the user who added the email to the queue.
    /// </summary>
    [Required]
    [StringLength(maximumLength: 256, MinimumLength = 3)]
    public string AddedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the email was last updated.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets the user who last updated the email.
    /// </summary>
    [StringLength(maximumLength: 256, MinimumLength = 3)]
    public string? UpdatedBy { get; set; }
}