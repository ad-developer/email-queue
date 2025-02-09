namespace EmailQueueCore.Client;

public interface IEmailClient
{
    EmailClientResult SendEmail(string? emailTo, string? emailToCC, string emailFrom, string emailTitle, string emailBody, bool isHtml);
    Task<EmailClientResult> SendEmailAsync(string? emailTo, string? emailToCC, string emailFrom, string emailTitle, string emailBody, bool isHtml,  CancellationToken cancellationToken);
}
