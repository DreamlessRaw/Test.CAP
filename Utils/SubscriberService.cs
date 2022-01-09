using System.Globalization;
using DotNetCore.CAP;
using Snowflake;
using Test.CAP.Database;
using Test.CAP.Models;
using Test.CAP.Utils;

namespace Test.CAP;

public interface ISubscriberService
{
    void LoginLog(PublishMessageModel<DateTime> data);
    void SubscribeMessage(DateTime dateTime);
}

public class SubscriberService : ISubscriberService, ICapSubscribe
{
    private readonly ILogger<SubscriberService> _logger;
    private readonly MssqlContext _mssqlContext;
    private readonly SnowFlake _snowFlake = new SnowFlake(1, 1);

    public SubscriberService(ILogger<SubscriberService> logger, MssqlContext mssqlContext)
    {
        _logger = logger;
        _mssqlContext = mssqlContext;
    }

    [CapSubscribe("xxx.services.show.time")]
    public void SubscribeMessage(DateTime dateTime)
    {
        _logger.LogError(dateTime.ToString(CultureInfo.InvariantCulture));
    }

    [CapSubscribe(nameof(SubscriberService.LoginLog))]
    public void LoginLog(PublishMessageModel<DateTime> data)
    {
        LoginLog log = new()
        {
            Id = _snowFlake.NextId(),
            AccountId = data.User.Id,
        };
        log.SaveOrUpdateOperation(data.User, data.Data);
        _mssqlContext.LoginLog.Add(log);
        _mssqlContext.SaveChanges();
    }
}