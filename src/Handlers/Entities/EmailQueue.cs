using EmailQueue.Validation;

namespace EmailQueue.Handlers.Entities;

public class EmailQueue
{
    public int Id { get; set; }
    public string EmailTo { get; set; }
    
    [EmailAddressList]
    public string EmailFrom { get; set; }
    public string EmailTitle { get; set; }
    public string EmailBody { get; set; }
    public bool IsHtml { get; set; }
    public string EmailDescription { get; set; }
    public Guid RefNumber { get; set; }
}
