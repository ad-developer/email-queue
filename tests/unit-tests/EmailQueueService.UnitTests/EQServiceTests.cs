using NSubstitute;
using emailqueueservice;
using EmailQueueCore;
using EmailQueueCore.Log;
using EmailQueueCore.Common;

namespace EmailQueueService.UnitEQServiceTests;

public class EQServiceEQServiceTests
{
    private readonly IEQService _eqService;
   private readonly IEQHandler _queueHandler;
    private readonly IEQLogHandler _logHandler;

    public EQServiceEQServiceTests()
    {
        _queueHandler = Substitute.For<IEQHandler>();
        _logHandler = Substitute.For<IEQLogHandler>();

        _eqService = new EQService(_queueHandler, _logHandler);
    }

    [Fact]
    public void AddEmailToQueue_ShouldReturnGuid()
    {
        // Arrange
        var emailFrom = "test@example.com";
        var emailTo = "recipient@example.com";
        var emailTitle = "Test Email";
        var emailBody = "This is a test email.";
        var emailDescription = "Test Description";
        var reffNumber = "12345";
        var requestor = "Test Requestor";
        var expectedGuid = Guid.NewGuid();

        _queueHandler.AddToQueue(emailFrom, emailTo, emailTitle, emailBody, emailDescription, reffNumber, requestor).Returns(expectedGuid);

        // Act
        var result = _eqService.AddEmailToQueue(emailFrom, emailTo, emailTitle, emailBody, emailDescription, reffNumber, requestor);

        // Assert
        Assert.Equal(expectedGuid, result);
        _queueHandler.Received(1).AddToQueue(emailFrom, emailTo, emailTitle, emailBody, emailDescription, reffNumber, requestor);
    }

    [Fact]
    public async Task AddEmailToQueueAsync_ShouldReturnGuid()
    {
        // Arrange
        var emailFrom = "test@example.com";
        var emailTo = "recipient@example.com";
        var emailTitle = "Test Email";
        var emailBody = "This is a test email.";
        var emailDescription = "Test Description";
        var reffNumber = "12345";
        var requestor = "Test Requestor";
        var expectedGuid = Guid.NewGuid();
        var cancellationToken = CancellationToken.None;

        _queueHandler.AddToQueueAsync(emailFrom, emailTo, emailTitle, emailBody, emailDescription, reffNumber, requestor, true, cancellationToken).Returns(Task.FromResult(expectedGuid));

        // Act
        var result = await _eqService.AddEmailToQueueAsync(emailFrom, emailTo, emailTitle, emailBody, emailDescription, reffNumber, requestor, true, cancellationToken);

        // Assert
        Assert.Equal(expectedGuid, result);
        await _queueHandler.Received(1).AddToQueueAsync(emailFrom, emailTo, emailTitle, emailBody, emailDescription, reffNumber, requestor, true, cancellationToken);
    }

    [Fact]
    public void GetEmailQueueLogById_ShouldReturnLogs()
    {
        // Arrange
        var emailQueueId = Guid.NewGuid();
        var expectedLogs = new List<EmailQueueLog>
        {
            new() { Id = Guid.NewGuid(), EmailQueueId = emailQueueId, RefNumber = "AAAB785",  Action =  EQActions.EmailQueueQueued, Description = "Log 1" },
            new() { Id = Guid.NewGuid(), EmailQueueId = emailQueueId, RefNumber = "AAAB785", Action =  EQActions.EmailQueueQueued, Description = "Log 2" }
        };

        _logHandler.GetLogByQueueId(emailQueueId).Returns(expectedLogs);

        // Act
        var result = _eqService.GetEmailQueueLogById(emailQueueId);

        // Assert
        Assert.Equal(expectedLogs, result);
        _logHandler.Received(1).GetLogByQueueId(emailQueueId);
    }

    [Fact]
    public async Task GetEmailQueueLogByIdAsync_ShouldReturnLogs()
    {
        // Arrange
        var emailQueueId = Guid.NewGuid();
        var expectedLogs = new List<EmailQueueLog>
        {
            new() { Id = Guid.NewGuid(), EmailQueueId = emailQueueId, RefNumber = "AAAB785",  Action =  EQActions.EmailQueueQueued, Description = "Log 1" },
            new() { Id = Guid.NewGuid(), EmailQueueId = emailQueueId, RefNumber = "AAAB785", Action =  EQActions.EmailQueueQueued, Description = "Log 2" }
        };

        _eqService.GetEmailQueueLogByIdAsync(emailQueueId).Returns(Task.FromResult<IEnumerable<EmailQueueLog>>(expectedLogs));

        // Act
        var result = await _eqService.GetEmailQueueLogByIdAsync(emailQueueId);

        // Assert
        Assert.Equal(expectedLogs, result);
        await _logHandler.Received(1).GetLogByQueueIdAsync(emailQueueId);
    }
}
