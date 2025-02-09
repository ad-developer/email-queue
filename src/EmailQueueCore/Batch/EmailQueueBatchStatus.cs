namespace EmailQueueCore.Batch;

public enum EmailQueueBatchStatus
{
    Queued = 1,
    Processing = 2,
    Failed = 3,
    FailedPermanent = 4,
    Completed = 5,
    ProcessingManually = 6,
}
