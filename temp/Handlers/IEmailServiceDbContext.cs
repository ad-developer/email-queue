using Microsoft.EntityFrameworkCore;

namespace EmailQueue.Handlers;

public interface IEmailServiceDbContext
{
    DbSet<Handlers.Entities.EmailQueue> EmailQueues { get; set;}
    int SaveChanges();
}
