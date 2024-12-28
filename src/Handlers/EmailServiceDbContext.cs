using Microsoft.EntityFrameworkCore;
using EmailQueue.Handlers.Entities;

namespace EmailQueue.Handlers;

public class EmailServiceDbContext : DbContext, IEmailServiceDbContext
{
    public virtual DbSet<Handlers.Entities.EmailQueue> EmailQueues { get; set;}
}