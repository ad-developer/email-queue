using EmailQueueCore.Common;

namespace EmailQueueCore.Log;

public static class EmailQueueLogExtentions
{
    public static void LogEmailQueueBatchProcessingStarted(this IEmailQueueLogRepository emailQueueLogRepository, 
        string refNumber, Guid emailQueueId, Guid? emailQueueBatchId, int? emailQueueBatchNumber, Guid? emailQueueBatchProcessId, bool reTry)
    {
        string? action;
        string? details;

        if (reTry)
        {
            action = EQActions.EmailBatchReTryProcessing;
            details =  $"Email batch #{emailQueueBatchNumber} with batch id{emailQueueBatchId} is reprocessing. Process id {emailQueueBatchProcessId}.";
        }
        else
        {
            action = EQActions.EmailBatchProcessing;
            details = $"Email batch #{emailQueueBatchNumber} with batch id{emailQueueBatchId} is processing. Process id {emailQueueBatchProcessId}.";
        }

        var logEntry = EmailQueueLog.Create(refNumber, emailQueueId, emailQueueBatchId, emailQueueBatchNumber, emailQueueBatchProcessId, action, details);
        emailQueueLogRepository.Add(logEntry);
    }

    public static void LogEmailQueueBatchProcessingCompleted(this IEmailQueueLogRepository emailQueueLogRepository, 
        string refNumber, Guid emailQueueId, Guid? emailQueueBatchId, int? emailQueueBatchNumber, Guid? emailQueueBatchProcessId )
    {
        
        var action = EQActions.EmailBatchCompleted;
        var details =  $"Email batch #{emailQueueBatchNumber} with batch id{emailQueueBatchId} is completed. Process id {emailQueueBatchProcessId}.";
      
        var logEntry = EmailQueueLog.Create(refNumber, emailQueueId, emailQueueBatchId, emailQueueBatchNumber, emailQueueBatchProcessId, action, details);
        emailQueueLogRepository.Add(logEntry);
    }

    public static void LogEmailQueueBatchProcessingFailed(this IEmailQueueLogRepository emailQueueLogRepository, 
        string refNumber, Guid emailQueueId, Guid? emailQueueBatchId, int? emailQueueBatchNumber, Guid? emailQueueBatchProcessId, string errorMessage )
    {
        
        var action = EQActions.EmailBatchFailed;
        var details =  $"Email batch #{emailQueueBatchNumber} with batch id{emailQueueBatchId} is failed. Process id {emailQueueBatchProcessId}. Error message: {errorMessage}";
      
        var logEntry = EmailQueueLog.Create(refNumber, emailQueueId, emailQueueBatchId, emailQueueBatchNumber, emailQueueBatchProcessId, action, details);
        emailQueueLogRepository.Add(logEntry);
    }

     public static void LogEmailQueueBatchProcessingFailedPermanent(this IEmailQueueLogRepository emailQueueLogRepository, 
        string refNumber, Guid emailQueueId, Guid? emailQueueBatchId, int? emailQueueBatchNumber, Guid? emailQueueBatchProcessId, string errorMessage )
    {
        
        var action = EQActions.EmailBatchFailedPermanent;
        var details =  $"Email batch #{emailQueueBatchNumber} with batch id{emailQueueBatchId} is failed permanent. Process id {emailQueueBatchProcessId}. Error message: {errorMessage}";
      
        var logEntry = EmailQueueLog.Create(refNumber, emailQueueId, emailQueueBatchId, emailQueueBatchNumber, emailQueueBatchProcessId, action, details);
        emailQueueLogRepository.Add(logEntry);
    }
}