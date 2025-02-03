using System;

namespace EmailQueueCore.Common;

public interface IUnitOfWork
{
    void SaveChanges();
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
