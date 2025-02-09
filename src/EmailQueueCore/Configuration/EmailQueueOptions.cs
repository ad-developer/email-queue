namespace EmailQueueCore.Configuration;

public class EmailQueueOptions
{
    public int BatchSize { get; set; }
    public int BatchProcessCount { get; set; }
    public int BatchProcessFailedCountMax { get; set; }
}
