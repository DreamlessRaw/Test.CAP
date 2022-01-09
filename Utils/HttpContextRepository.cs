using System.Security.Claims;
using Newtonsoft.Json;

namespace Test.CAP.Utils;

public interface IHttpContextRepository
{
    Microsoft.AspNetCore.Http.HttpContext? GetHttpContext();

    LoginUser User();
}

public class HttpContextRepository:IHttpContextRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextRepository(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Microsoft.AspNetCore.Http.HttpContext? GetHttpContext()
    {
        return _httpContextAccessor.HttpContext;
    }

    LoginUser IHttpContextRepository.User()
    {
        if (HttpContext.Context == null || HttpContext.Context.User.Identity == null) throw new Exception("请登录");
        if (!HttpContext.Context.User.Identity.IsAuthenticated ||
            HttpContext.Context.User.Identity is not ClaimsIdentity identity)
            throw new Exception("请登录");
        if (int.TryParse(identity.Claims.FirstOrDefault(m => m.Type == ClaimTypes.PrimarySid)?.Value,
                out var id) && id > 0)
            return JsonConvert.DeserializeObject<LoginUser>(identity.Claims
                .FirstOrDefault(m => m.Type == ClaimTypes.UserData)?.Value);

        throw new Exception("请登录");
    }

}