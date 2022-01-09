using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Snowflake;
using Test.CAP.Database;
using Test.CAP.Models;
using Test.CAP.Utils;

namespace Test.CAP.Controllers;

public class LoginController : Controller
{
    private readonly MssqlContext _mssqlContext;
    private readonly SnowFlake _snowFlake = new(1, 1);
    private readonly ICapPublisher _capBus;

    public LoginController(MssqlContext mssqlContext, ICapPublisher capBus)
    {
        _mssqlContext = mssqlContext;
        _capBus = capBus;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Route("initAccount")]
    [HttpPost]
    public ApiResult InitAccount()
    {
        _mssqlContext.Account.Add(new Account()
        {
            Id = _snowFlake.NextId(),
            Code = "Dreamless",
            Name = "三餘、無夢生",
            Password = Md5Utils.GetMD5String("123456"),
            CreateId = 1,
            CreateName = "三餘、無夢生",
            CreateTime = DateTime.Now,
            ModifyId = 1,
            ModifyName = "三餘、無夢生",
            ModifyTime = DateTime.Now,
        });
        var result = _mssqlContext.SaveChanges() > 0;
        return result ? ApiResult.Ok(true) : ApiResult.Error("操作失败!!!");
    }

    [Route("login/{name}/{password}")]
    [HttpPost]
    public ApiResult Login(string name, string password)
    {
        var result = _mssqlContext.Account.FirstOrDefault(f =>
            f.Code.Equals(name) && f.Password.Equals(Md5Utils.GetMD5String(password)));
        if (result == null) return ApiResult.Error("用户不存在!!!");
        return result.IsDelete ? ApiResult.Error("用户已被删除!!!") : LoginResult(result);
    }

    [NonAction]
    private ApiResult LoginResult(Account account)
    {
        var user = new LoginUser
        {
            Id = account.Id,
            Code = account.Code,
            Name = account.Name
        };
        var claim = new[]
        {
            new Claim(ClaimTypes.PrimarySid, account.Id.ToString()),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user))
        };
        //对称秘钥
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Instance.SecretKey));
        //签名证书(秘钥，加密算法)
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(JwtSettings.Instance.Issuer, JwtSettings.Instance.Audience, claim,
            DateTime.Now, DateTime.Now.AddDays(1), cred);
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        // 登录日志
        _capBus.Publish(nameof(SubscriberService.LoginLog),
            new PublishMessageModel<DateTime>(user, DateTime.Now));
        return ApiResult.Ok(jwt);
    }
}