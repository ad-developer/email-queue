namespace EmailQueueCore.Common;

public sealed class EQActions
{
    public const string EmailQueueQueued = "Email-Queue:Queued";
    public const string EmailQueueScheduling = "Email-Queue:Scheduling";
    public const string EmailQueueScheduled = "Email-Queue:Scheduled";
    public const string EmailQueueCompleted = "Email-Queue:Completed";
}
