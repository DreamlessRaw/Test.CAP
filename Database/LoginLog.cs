using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.CAP.Database;

[Table("LoginLog")]
public class LoginLog : BaseEntity
{
    [Key] public long Id { get; set; }
    public long AccountId { get; set; }
}