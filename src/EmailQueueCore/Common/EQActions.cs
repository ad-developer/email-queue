namespace EmailQueueCore.Common;

public sealed class EQActions
{
    public const string EmailQueueQueued = "Email-Queue:Queued";
    public const string EmailQueueScheduling = "Email-Queue:Scheduling";
    public const string EmailQueueScheduled = "Email-Queue:Scheduled";
    public const string EmailQueueCompleted = "Email-Queue:Completed";

    public const string EmailBatchQueued = "Email-Batch:Queued";
    public const string EmailBatchProcessing = "Email-Batch:Processing";
    public const string EmailBatchFailed = "Email-Batch:Failed";
    public const string EmailBatchReTryProcessing = "Email-Batch:ReTryProcessing";
    public const string EmailBatchCompleted = "Email-Batch:Failed";
}
