using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.CompilerServices;
using Savorboard.CAP.InMemoryMessageQueue;
using Test.CAP;
using Test.CAP.Database;
using Test.CAP.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ISubscriberService, SubscriberService>();
builder.Services.AddDbContext<MssqlContext>(opt =>
    opt.UseSqlServer(builder.Configuration["CoreConfig:SqlServerConnectionString"]));
builder.Services.AddCap(cap =>
{
    cap.UseEntityFramework<MssqlContext>();
    cap.UseRabbitMQ(builder.Configuration["CoreConfig:MqConnectionString"]);
    // cap.UseInMemoryStorage();
    // cap.UseInMemoryMessageQueue();
    cap.UseDashboard();
});
// 内存缓存
builder.Services.AddMemoryCache();
//Jwt
JwtSettings.Instance = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
// 集成cookie和token
builder.Services.AddAuthentication(options =>
    {
        //认证middleware配置
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    // .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    // .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt => { opt.LoginPath = "/login/index"; })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
    {
        //主要是jwt  token参数设置
        o.TokenValidationParameters = new TokenValidationParameters
        {
            //Token颁发机构
            ValidIssuer = JwtSettings.Instance.Issuer,
            //颁发给谁
            ValidAudience = JwtSettings.Instance.Audience,
            //这里的key要进行加密，需要引用Microsoft.IdentityModel.Tokens
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Instance.SecretKey))
            //ValidateIssuerSigningKey=true,
            ////是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
            //ValidateLifetime=true,
            ////允许的服务器时间偏移量
            //ClockSkew=TimeSpan.Zero
        };
    });

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
// builder.Services
//     .AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor,
//         Microsoft.AspNetCore.Http.HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
var context = app.Services.GetRequiredService<IHttpContextAccessor>();
Test.CAP.Utils.HttpContext.Configure(context);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();