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

    public EQBatchHandler(IEmailQueueBatchRepository emailQueueBatchRepository, IEmailQueueRepository emailQueueRepository, 
        IEmailQueueLogRepository emailQueueLogRepository, IOptions<EmailQueueOptions> emailQueueOptions)
    {
        _emailQueueBatchRepository = emailQueueBatchRepository;
        _emailQueueRepository = emailQueueRepository;
        _emailQueueLogRepository = emailQueueLogRepository;
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

        _emailQueueLogRepository.Add(new EmailQueueLog
        {
            EmailQueueId = lastEmailQueuue.Id,
            RefNumber = lastEmailQueuue.RefNumber,
            Action =    EQActions.EmailQueueScheduling,
            Description = EQActions.EmailQueueScheduling
        });

        SaveChanges();
        
        for (int i = 0; i < batchCount; i++)
        {
            // Batch queue preparation
            var batchId = Guid.NewGuid();
            var skip = i * _emailQueueOptions.BatchSize;
            var batckNumber = i + 1;
           
            // Batch queue
            _emailQueueBatchRepository.Add(new EmailQueueBatch
            {
                Id = batchId,
                EmailQueueId = lastEmailQueuue.Id,
                BatchNumber = batckNumber,
                
                EmailFrom = lastEmailQueuue.EmailFrom,
                //EmailTo = string.Join(";", emailToList.Skip(i * _emailQueueOptions.BatchSize).Take(_emailQueueOptions.BatchSize)),
                EmailTo = string.Join(";", emailToList[skip.._emailQueueOptions.BatchSize]),
                EmailTitle = lastEmailQueuue.EmailTitle,
                EmailBody = lastEmailQueuue.EmailBody,

                Status = EmailQueueStatus.Queued,

                AddedDate = DateTime.UtcNow,
            });

            // Log
            _emailQueueLogRepository.Add(new EmailQueueLog
            {
                EmailQueueId = lastEmailQueuue.Id,
                RefNumber = lastEmailQueuue.RefNumber,
                Action =    EQActions.EmailBatchQueued,
                Description = $"Email Batch # {batckNumber} with id {batchId} queued"
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

        _emailQueueLogRepository.Add(new EmailQueueLog
        {
            EmailQueueId = lastEmailQueuue.Id,
            RefNumber = lastEmailQueuue.RefNumber,
            Action =    EQActions.EmailQueueScheduling,
            Description = EQActions.EmailQueueScheduling
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
                
                EmailFrom = lastEmailQueuue.EmailFrom,
                //EmailTo = string.Join(";", emailToList.Skip(i * _emailQueueOptions.BatchSize).Take(_emailQueueOptions.BatchSize)),
                EmailTo = string.Join(";", emailToList[skip.._emailQueueOptions.BatchSize]),
                EmailTitle = lastEmailQueuue.EmailTitle,
                EmailBody = lastEmailQueuue.EmailBody,

                Status = EmailQueueStatus.Queued,

                AddedDate = DateTime.UtcNow,
            }, cancellationToken);
        
             // Log
            await _emailQueueLogRepository.AddAsync(new EmailQueueLog
            {
                EmailQueueId = lastEmailQueuue.Id,
                RefNumber = lastEmailQueuue.RefNumber,
                Action =    EQActions.EmailBatchQueued,
                Description = $"Email Batch # {batckNumber} with id {batchId} queued"
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
