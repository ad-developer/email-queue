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
    /// <param name="emailTo">The recipient email address. This address can be either a single email address or multiple email addresses separated by a semicolon.</param>
    /// <param name="emailFrom">The sender email address.</param>
    /// <param name="emailTitle">The title of the email.</param>
    /// <param name="emailBody">The body content of the email.</param>
    /// <param name="isHtml">Indicates whether the email body is in HTML format.</param>
    /// <param name="emailDescription">A description of the email.</param>
    /// <param name="refNumber">A reference number associated with the email.</param>
    public EmailQueue(string emailTo, string emailFrom, string emailTitle, string emailBody, bool isHtml, string emailDescription, Guid refNumber)
    {
        EmailTo = emailTo;
        EmailFrom = emailFrom;
        EmailTitle = emailTitle;
        EmailBody = emailBody;
        IsHtml = isHtml;
        EmailDescription = emailDescription;
        RefNumber = refNumber;
    }

    /// <summary>
    /// Gets or sets the unique identifier for the email queue entry.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the recipient email address.
    /// </summary>
    public string EmailTo { get; set; }

    /// <summary>
    /// Gets or sets the sender email address. This address can be either a single email address or multiple email addresses separated by a semicolon.
    /// </summary>
    [EmailAddressList]
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
    public bool IsHtml { get; set; }

    /// <summary>
    /// Gets or sets a description of the email.
    /// </summary>
    public string EmailDescription { get; set; }

    /// <summary>
    /// Gets or sets the reference number associated with the email.
    /// </summary>
    public Guid RefNumber { get; set; }
}