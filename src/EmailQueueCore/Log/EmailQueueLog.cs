using System.ComponentModel.DataAnnotations;
using EmailQueueCore.Common;

namespace EmailQueueCore;

public class EmailQueueLog : IEntity
{
    [Key]
    public Guid Id { get; set; }
    public required string RefNumber { get; set; }
    public Guid EmailQueueId { get; set; }   
    public required string Action { get; set; }  
    public required string Description { get; set; }
}
