using Microsoft.EntityFrameworkCore;
using EmailQueue.Handlers.Entities;

namespace EmailQueue.Handlers;

public class EmailServiceDbContext : DbContext, IEmailServiceDbContext
{
    public virtual required DbSet<Handlers.Entities.EmailQueue> EmailQueues { get; set;}
    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
}