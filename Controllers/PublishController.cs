using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Test.CAP.Controllers;

[Authorize]
[ApiController]
public class PublishController : Controller
{
    private readonly ILogger<PublishController> _logger;

    private readonly ICapPublisher _capBus;

    public PublishController(ICapPublisher capPublisher, ILogger<PublishController> logger)
    {
        _capBus = capPublisher;
        _logger = logger;
    }

    [Route("/publish")]
    public IActionResult PublishMessage()
    {
        _capBus.Publish("xxx.services.show.time", DateTime.Now);
        return Ok();
    }
}