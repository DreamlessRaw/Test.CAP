using System.Security.Claims;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using Test.CAP.Models;

namespace Test.CAP.Utils;

public static class HttpContext
{
    private static IHttpContextAccessor _contextAccessor;

    public static Microsoft.AspNetCore.Http.HttpContext Context => _contextAccessor.HttpContext!;

    public static void Configure(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
}

public static class Current
{
    public static string ClientIp =>
        HttpContext.Context.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString() ?? "127.0.0.1";

    public static LoginUser User
    {
        get
        {
            if (HttpContext.Context == null || HttpContext.Context.User.Identity == null) throw new Exception("请登录");
            if (!HttpContext.Context.User.Identity.IsAuthenticated ||
                HttpContext.Context.User.Identity is not ClaimsIdentity identity)
                throw new Exception("请登录");
            if (long.TryParse(identity.Claims.FirstOrDefault(m => m.Type == ClaimTypes.PrimarySid)?.Value,
                    out var id) && id > 0)
                return JsonConvert.DeserializeObject<LoginUser>(identity.Claims
                    .FirstOrDefault(m => m.Type == ClaimTypes.UserData)?.Value);

            throw new Exception("请登录");
        }
    }
}

public class LoginUser
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
}