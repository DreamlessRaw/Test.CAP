using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.CAP.Database;

[Table("Account")]
public class Account : BaseEntity
{
    [Key] public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
}