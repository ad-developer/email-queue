using EmailQueueCore.Common;

namespace EmailQueueCore.Log;

public static class EmailQueueLogExtentions
{
    public static void LogEmailQueueQueued(this IEmailQueueLogRepository emailQueueLogRepository, 
        string refNumber, Guid emailQueueId )
    {
        
        var action = EQActions.EmailQueueQueued;
        var details =  $"Email Queue queued {emailQueueId} with reference number {refNumber}";
      
        var logEntry = EmailQueueLog.Create(refNumber, emailQueueId, action, details);
        emailQueueLogRepository.Add(logEntry);
    }

    public static async Task LogEmailQueueQueuedAsync(this IEmailQueueLogRepository emailQueueLogRepository, 
        string refNumber, Guid emailQueueId, CancellationToken cancellationToken )
    {
        
        var action = EQActions.EmailQueueQueued;
        var details =  $"Email Queue queued {emailQueueId} with reference number {refNumber}";
      
        var logEntry = EmailQueueLog.Create(refNumber, emailQueueId, action, details);
        await emailQueueLogRepository.AddAsync(logEntry, cancellationToken);
    }

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

    public static async Task LogEmailQueueBatchProcessingStartedAsync(this IEmailQueueLogRepository emailQueueLogRepository, 
        string refNumber, Guid emailQueueId, Guid? emailQueueBatchId, int? emailQueueBatchNumber, Guid? emailQueueBatchProcessId, bool reTry, CancellationToken cancellationToken = default)
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
        await emailQueueLogRepository.AddAsync(logEntry, cancellationToken);
    }
    
    public static async Task LogEmailQueueBatchProcessingCompletedAsync(this IEmailQueueLogRepository emailQueueLogRepository, 
        string refNumber, Guid emailQueueId, Guid? emailQueueBatchId, int? emailQueueBatchNumber, Guid? emailQueueBatchProcessId, CancellationToken cancellationToken = default )
    {
        
        var action = EQActions.EmailBatchCompleted;
        var details =  $"Email batch #{emailQueueBatchNumber} with batch id{emailQueueBatchId} is completed. Process id {emailQueueBatchProcessId}.";
      
        var logEntry = EmailQueueLog.Create(refNumber, emailQueueId, emailQueueBatchId, emailQueueBatchNumber, emailQueueBatchProcessId, action, details);
        await emailQueueLogRepository.AddAsync(logEntry, cancellationToken);
    }

    public static async Task LogEmailQueueBatchProcessingFailedPermanentAsync(this IEmailQueueLogRepository emailQueueLogRepository, 
        string refNumber, Guid emailQueueId, Guid? emailQueueBatchId, int? emailQueueBatchNumber, Guid? emailQueueBatchProcessId, string errorMessage, CancellationToken cancellationToken = default )
    {
        
        var action = EQActions.EmailBatchFailedPermanent;
        var details =  $"Email batch #{emailQueueBatchNumber} with batch id{emailQueueBatchId} is failed permanent. Process id {emailQueueBatchProcessId}. Error message: {errorMessage}";
      
        var logEntry = EmailQueueLog.Create(refNumber, emailQueueId, emailQueueBatchId, emailQueueBatchNumber, emailQueueBatchProcessId, action, details);
        await emailQueueLogRepository.AddAsync(logEntry, cancellationToken);
    }

    public static async Task LogEmailQueueBatchProcessingFailedAsync(this IEmailQueueLogRepository emailQueueLogRepository, 
        string refNumber, Guid emailQueueId, Guid? emailQueueBatchId, int? emailQueueBatchNumber, Guid? emailQueueBatchProcessId, string errorMessage, CancellationToken cancellationToken = default )
    {
        
        var action = EQActions.EmailBatchFailed;
        var details =  $"Email batch #{emailQueueBatchNumber} with batch id{emailQueueBatchId} is failed. Process id {emailQueueBatchProcessId}. Error message: {errorMessage}";
      
        var logEntry = EmailQueueLog.Create(refNumber, emailQueueId, emailQueueBatchId, emailQueueBatchNumber, emailQueueBatchProcessId, action, details);
        await emailQueueLogRepository.AddAsync(logEntry, cancellationToken);
    }

}