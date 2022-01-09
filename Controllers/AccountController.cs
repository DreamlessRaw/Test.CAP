using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.CAP.Database;
using Test.CAP.Models;

namespace Test.CAP.Controllers;

[Authorize]
[ApiController]
[Route("Account")]
public class AccountController : Controller
{
    private readonly MssqlContext _mssqlContext;

    public AccountController(MssqlContext mssqlContext)
    {
        _mssqlContext = mssqlContext;
    }

    [Route("list/{page:int}/{pageSize:int}")]
    public ApiResult GetList(int page, int pageSize)
    {
        var data = _mssqlContext.Account.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return ApiResult.Ok(data);
    }
}