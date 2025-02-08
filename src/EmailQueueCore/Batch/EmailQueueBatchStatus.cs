namespace EmailQueueCore.Batch;

public enum EmailQueueBatchStatus
{
    Queued = 1,
    ProcessStarted = 2,
    ProcessEnded = 3,
    Failed = 4,
    FailedPermanent = 5,
    Completed = 6
}
