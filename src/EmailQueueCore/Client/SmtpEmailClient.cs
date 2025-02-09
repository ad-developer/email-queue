

namespace EmailQueueCore.Client;

public class SmtpEmailClient : IEmailClient
{
    public EmailClientResult SendEmail(string? emailTo, string? emailToCC, string emailFrom, string emailTitle, string emailBody, bool isHtml)
    {
        throw new NotImplementedException();
    }

    public Task<EmailClientResult> SendEmailAsync(string? emailTo, string? emailToCC, string emailFrom, string emailTitle, string emailBody, bool isHtml, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
