namespace EmailQueueCore.Client;

public class EmailClientResult
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
    public string? BouncedEmails { get; set; }
}
