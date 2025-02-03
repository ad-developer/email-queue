using System.ComponentModel.DataAnnotations;
using EmailQueueCore.Common;
using EmailQueueCore.Log;


namespace EmailQueueCore;

public class EQHandler : IEQHandler, IUnitOfWork
{
    private readonly IEmailQueueRepository _emailQueueRepository;
    private readonly IEmailQueueLogRepository _emailQueueLogRepository;
    private readonly IEQContext _context;

    
    public EQHandler(IEmailQueueRepository emailQueueRepository, IEmailQueueLogRepository emailQueueLogRepository, IEQContext context)
    {
        _emailQueueRepository = emailQueueRepository;
        _emailQueueRepository.Context = context;
        _emailQueueRepository.SaveChanges = false;
        
        _emailQueueLogRepository = emailQueueLogRepository;
        _emailQueueLogRepository.Context = context;
        _emailQueueLogRepository.SaveChanges = false;

        _context = context;
    }
   
    public Guid AddToQueue(string emailFrom, string emailTo, string emailTitle, string emailBody, string emailDescription, string refNumber, string requestor, bool isHtmlBody = true)
    {
        var eqId = _emailQueueRepository.Add(new EmailQueue(emailTo, emailFrom, emailTitle, emailBody, isHtmlBody, emailDescription, refNumber, requestor)).Id;
        SaveChanges();

        var logEntry = new EmailQueueLog {
            RefNumber = refNumber, 
            EmailQueueId = eqId, 
            Action = EQActions.EmailQueueQueued,
            Description = EQActions.EmailQueueQueued
        };
        
        _emailQueueLogRepository.Add(logEntry);
        SaveChanges();

        return eqId;
    }

    public async Task<Guid> AddToQueueAsync(string emailFrom, string emailTo, string emailTitle, string emailBody, string emailDescription, string refNumber, string requestor, bool isHtmlBody = true, CancellationToken cancellationToken = default)
    {
        var eqId = await _emailQueueRepository.AddAsync(new EmailQueue(emailTo, emailFrom, emailTitle, emailBody, isHtmlBody, emailDescription, refNumber, requestor), cancellationToken)
            .ContinueWith(t => t.Result.Id);
        await SaveChangesAsync(cancellationToken);
        
        var logEntry = new EmailQueueLog {
            RefNumber = refNumber, 
            EmailQueueId = eqId, 
            Action = EQActions.EmailQueueQueued,
            Description = EQActions.EmailQueueQueued
        };
        
        await _emailQueueLogRepository.AddAsync(logEntry, cancellationToken);
        await SaveChangesAsync(cancellationToken);

        return eqId;
    }

    public EmailQueue GetFromQueue(Guid emailQueueId)
    {
        return _emailQueueRepository.GetById(emailQueueId);
    }

    public Task<EmailQueue> GetFromQueueAsync(Guid emailQueueId)
    {
        return _emailQueueRepository.GetByIdAsync(emailQueueId);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
