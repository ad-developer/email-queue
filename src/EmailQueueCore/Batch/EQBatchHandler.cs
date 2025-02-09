using EmailQueueCore.Client;
using EmailQueueCore.Common;
using EmailQueueCore.Configuration;
using EmailQueueCore.Log;
using EmailQueueCore.Queue;
using Microsoft.Extensions.Options;

namespace EmailQueueCore.Batch;

public class EQBatchHandler : IEQBatchHandler, IUnitOfWork
{
    private readonly IEmailQueueBatchRepository _emailQueueBatchRepository;
    private readonly IEmailQueueRepository _emailQueueRepository;
    private readonly IEmailQueueLogRepository   _emailQueueLogRepository;
    private readonly EmailQueueOptions _emailQueueOptions;
    private readonly IEmailClient _emailClient;



    public EQBatchHandler(IEmailQueueBatchRepository emailQueueBatchRepository, IEmailQueueRepository emailQueueRepository, 
        IEmailQueueLogRepository emailQueueLogRepository, IEmailClient emailClient, IOptions<EmailQueueOptions> emailQueueOptions)
    {
        _emailQueueBatchRepository = emailQueueBatchRepository;
        _emailQueueRepository = emailQueueRepository;
        _emailQueueLogRepository = emailQueueLogRepository;
        _emailClient = emailClient;
        _emailQueueOptions = emailQueueOptions.Value;
    }
   
    public void ScheduleBatches()
    {
        var lastEmailQueuue = _emailQueueRepository.GetNextEmailQueue();
        if (lastEmailQueuue == null)
        {
            return;
        }

        var emailToList = lastEmailQueuue.EmailTo.Split(';');
        var batchCount = (int)Math.Ceiling((double)emailToList.Length / _emailQueueOptions.BatchSize);
        
        lastEmailQueuue.Status = EmailQueueStatus.Scheduling;
        _emailQueueRepository.Update(lastEmailQueuue);

        // Log
        _emailQueueLogRepository.Add(new EmailQueueLog
        {
            EmailQueueId = lastEmailQueuue.Id,
            RefNumber = lastEmailQueuue.RefNumber,
            Action =    EQActions.EmailQueueScheduling,
            Details = EQActions.EmailQueueScheduling
        });

        SaveChanges();
        
        for (int i = 0; i < batchCount; i++)
        {
            // Batch queue preparation
            var batchId = Guid.NewGuid();
            var skip = i * _emailQueueOptions.BatchSize;
            var batcNumber = i + 1;
           
            // Batch queue
            _emailQueueBatchRepository.Add(new EmailQueueBatch
            {
                Id = batchId,
                EmailQueueId = lastEmailQueuue.Id,
                RefNumber = lastEmailQueuue.RefNumber,
                BatchNumber = batcNumber,
                
                EmailFrom = lastEmailQueuue.EmailFrom,
                //EmailTo = string.Join(";", emailToList.Skip(i * _emailQueueOptions.BatchSize).Take(_emailQueueOptions.BatchSize)),
                EmailTo = string.Join(";", emailToList[skip.._emailQueueOptions.BatchSize]),
                EmailTitle = lastEmailQueuue.EmailTitle,
                EmailBody = lastEmailQueuue.EmailBody,

                Status = EmailQueueBatchStatus.Queued,

                AddedDate = DateTime.UtcNow,
            });

            // Log
            _emailQueueLogRepository.Add(new EmailQueueLog
            {
                EmailQueueId = lastEmailQueuue.Id,
                RefNumber = lastEmailQueuue.RefNumber,
                Action =    EQActions.EmailBatchQueued,
                Details = $"Email Batch # {batcNumber} with id {batchId} queued"
            });
            
        }
        
        lastEmailQueuue.Status = EmailQueueStatus.Scheduled;
        lastEmailQueuue.UpdatedDate = DateTime.UtcNow;
        lastEmailQueuue.UpdatedBy = "System";

        _emailQueueRepository.Update(lastEmailQueuue);
       
        SaveChanges();
    }

    public async Task ScheduleBatchesAsync(CancellationToken cancellationToken  = default)
    {
        var lastEmailQueuue = _emailQueueRepository.GetNextEmailQueue();
        if (lastEmailQueuue == null)
        {
            return;
        }

        var emailToList = lastEmailQueuue.EmailTo.Split(';');
        var batchCount = (int)Math.Ceiling((double)emailToList.Length / _emailQueueOptions.BatchSize);
        
        lastEmailQueuue.Status = EmailQueueStatus.Scheduling;
        lastEmailQueuue.UpdatedDate = DateTime.UtcNow;
        lastEmailQueuue.UpdatedBy = "System";

        await _emailQueueRepository.UpdateAsync(lastEmailQueuue, cancellationToken);

        // Log
        _emailQueueLogRepository.Add(new EmailQueueLog
        {
            EmailQueueId = lastEmailQueuue.Id,
            RefNumber = lastEmailQueuue.RefNumber,
            Action =    EQActions.EmailQueueScheduling,
            Details = EQActions.EmailQueueScheduling
        });
        await SaveChangesAsync(cancellationToken);
        
        for (int i = 0; i < batchCount; i++)
        {
            // Batch queue preparation
            var batchId = Guid.NewGuid();
            var skip = i * _emailQueueOptions.BatchSize;
            var batckNumber = i + 1;
           
            // Batch queue          
            await _emailQueueBatchRepository.AddAsync(new EmailQueueBatch
            {
                Id = batchId,
                EmailQueueId = lastEmailQueuue.Id,
                BatchNumber = batckNumber,
                RefNumber = lastEmailQueuue.RefNumber,

                EmailFrom = lastEmailQueuue.EmailFrom,
                //EmailTo = string.Join(";", emailToList.Skip(i * _emailQueueOptions.BatchSize).Take(_emailQueueOptions.BatchSize)),
                EmailTo = string.Join(";", emailToList[skip.._emailQueueOptions.BatchSize]),
                EmailTitle = lastEmailQueuue.EmailTitle,
                EmailBody = lastEmailQueuue.EmailBody,

                Status = EmailQueueBatchStatus.Queued,

                AddedDate = DateTime.UtcNow,
            }, cancellationToken);
        
             // Log
            await _emailQueueLogRepository.AddAsync(new EmailQueueLog
            {
                EmailQueueId = lastEmailQueuue.Id,
                RefNumber = lastEmailQueuue.RefNumber,
                Action =    EQActions.EmailBatchQueued,
                Details = $"Email Batch # {batckNumber} with id {batchId} queued"
            }, cancellationToken);
        }
        
        lastEmailQueuue.Status = EmailQueueStatus.Scheduled;
        lastEmailQueuue.UpdatedDate = DateTime.UtcNow;
        lastEmailQueuue.UpdatedBy = "System";
        
        await _emailQueueRepository.UpdateAsync(lastEmailQueuue, cancellationToken);
       
        await SaveChangesAsync(cancellationToken);
    }
    
    public void ProcessBatches()
    {
        // Process failed batches  
        ProcessBatchesFailed();

        // Process queued batches  
        ProcessBatchesQueued();
    }

    internal void ProcessBatchesFailed()
    {
        var batchProcessId = Guid.NewGuid();

        // Get next failed batches
        var failedBatches = _emailQueueBatchRepository.GetNextEmailQueueBatchFailedList();
        if (failedBatches.Any())
        {
            foreach (var failedBatch in failedBatches)
            {
                try {

                    // Setting the batch status to process started
                    failedBatch.Status = EmailQueueBatchStatus.Processing;
                    failedBatch.ProcessStarted = DateTime.UtcNow;
                    failedBatch.LastBatchProcessId = batchProcessId;
                    failedBatch.ProcessedCount ++;

                    _emailQueueBatchRepository.Update(failedBatch);
                    
                    //Log
                    _emailQueueLogRepository.LogEmailQueueBatchProcessingStarted(failedBatch.RefNumber, failedBatch.EmailQueueId, 
                        failedBatch.Id, failedBatch.BatchNumber, batchProcessId, true);
                    
                    SaveChanges();
                
                    // Process the batch
                    var result = _emailClient.SendEmail(null, failedBatch.EmailFrom, failedBatch.EmailTo, failedBatch.EmailTitle, failedBatch.EmailBody, failedBatch.IsHtml);
                    // Setting the batch resulted in error
                    if(result.IsSuccess){
                         // Setting the batch status to process ended
                        failedBatch.Status = EmailQueueBatchStatus.Completed;
                        failedBatch.ProcessEnded = DateTime.UtcNow;
                        if(!string.IsNullOrEmpty(result.BouncedEmails))
                        {
                            failedBatch.EmailToFailed = result.BouncedEmails;
                        }
                        _emailQueueBatchRepository.Update(failedBatch);
                        
                        // Log 
                        _emailQueueLogRepository.LogEmailQueueBatchProcessingCompleted(failedBatch.RefNumber, failedBatch.EmailQueueId, 
                            failedBatch.Id, failedBatch.BatchNumber, batchProcessId);

                        SaveChanges();
                    }
                    else
                    {
                        // Setting the batch status to failed
                        // If the batch has failed more than the max count, set it to failed permanent
                        if(failedBatch.FailedCount + 1 == _emailQueueOptions.BatchProcessFailedCountMax)
                        {
                            failedBatch.Status = EmailQueueBatchStatus.FailedPermanent;

                             // Log 
                            _emailQueueLogRepository.LogEmailQueueBatchProcessingFailedPermanent(failedBatch.RefNumber, failedBatch.EmailQueueId, 
                                failedBatch.Id, failedBatch.BatchNumber, batchProcessId, result.ErrorMessage);
                        }
                        else
                        {
                            failedBatch.Status = EmailQueueBatchStatus.Failed;
                            
                            // Log 
                            _emailQueueLogRepository.LogEmailQueueBatchProcessingFailed(failedBatch.RefNumber, failedBatch.EmailQueueId, 
                                failedBatch.Id, failedBatch.BatchNumber, batchProcessId, result.ErrorMessage);
                        }
                        
                        failedBatch.LastFailureMessage = result.ErrorMessage;
                        failedBatch.FailedCount ++;
                        _emailQueueBatchRepository.Update(failedBatch);
                        
                        SaveChanges();
                    }
                }   
                catch(Exception ex)
                {
                    // Setting the batch status to failed
                    // If the batch has failed more than the max count, set it to failed permanent
                    if(failedBatch.FailedCount + 1 == _emailQueueOptions.BatchProcessFailedCountMax)
                    {
                        failedBatch.Status = EmailQueueBatchStatus.FailedPermanent;
                        
                        // Log 
                        _emailQueueLogRepository.LogEmailQueueBatchProcessingFailedPermanent(failedBatch.RefNumber, failedBatch.EmailQueueId, 
                            failedBatch.Id, failedBatch.BatchNumber, batchProcessId, ex.Message);
                    }
                    else
                    {
                        failedBatch.Status = EmailQueueBatchStatus.Failed;
                        
                        // Log 
                        _emailQueueLogRepository.LogEmailQueueBatchProcessingFailed(failedBatch.RefNumber, failedBatch.EmailQueueId, 
                            failedBatch.Id, failedBatch.BatchNumber, batchProcessId, ex.Message);
                    }
                    
                    failedBatch.LastFailureMessage = ex.Message;
                    failedBatch.FailedCount ++;
                    _emailQueueBatchRepository.Update(failedBatch);
                    
                    SaveChanges();
                }
            }
        }
    }
    
    internal void ProcessBatchesQueued()
    {

    }
    
    public async Task ProcessBatchesAsync()
    {
        
    }

    public void SaveChanges()
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
