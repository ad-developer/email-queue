using EmailQueueCore;
using EmailQueueCore.Batch;
using EmailQueueCore.Client;
using EmailQueueCore.Configuration;
using EmailQueueCore.Log;
using EmailQueueWorker.QueueWorker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmailQueueWorker;

public static class EmailQueueWorkerExtentions
{
    public static void ConfigureQueueHostedServices(this IHostBuilder builder, Action<EmailQueueWorkerOptions> options){
        builder.ConfigureServices((hostContext, services) =>{
            
            services.Configure<EmailQueueOptions>(hostContext.Configuration.GetSection("EmailQueueOptions"));
            
            ArgumentNullException.ThrowIfNull(options, nameof(options));
            
            EmailQueueWorkerOptions op = new();
            options(op);
            ArgumentException.ThrowIfNullOrEmpty(op.ConnectionName, op.ConnectionName);

            services.AddDbContext<EQContext>(options=>{
                var connectionString =
                    hostContext.Configuration.GetConnectionString(op.ConnectionName);
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IEmailClient, SmtpEmailClient>();
            services.AddScoped<IEmailQueueLogRepository,EmailQueueLogRepository>();
            services.AddScoped<IEmailQueueRepository, EmailQueueRepository>();
            services.AddScoped<IEQContext, EQContext>();
            services.AddScoped<IEmailQueueBatchRepository,EmailQueueBatchRepository>();
            services.AddScoped<IEQBatchHandler, EQBatchHandler>();
        
            services.AddHostedService<QueueProcessingHostedService>();
        });
    }
}