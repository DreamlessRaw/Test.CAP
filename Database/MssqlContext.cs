using Microsoft.EntityFrameworkCore;

namespace Test.CAP.Database;

public class MssqlContext : DbContext
{
    public MssqlContext(DbContextOptions<MssqlContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine).EnableSensitiveDataLogging().EnableDetailedErrors();
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Account> Account { get; set; }
    public DbSet<LoginLog> LoginLog { get; set; }

}