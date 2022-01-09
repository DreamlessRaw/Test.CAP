using System;
using System.ComponentModel.DataAnnotations;

namespace Test.CAP.Database
{
    public class BaseEntity : IBaseEntity
    {
        public long Id { get; set; }
        public bool IsDelete { get; set; }
        public long CreateId { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateTime { get; set; }
        public long ModifyId { get; set; }
        public string ModifyName { get; set; }
        public DateTime ModifyTime { get; set; }
    }

    public interface IBaseEntity
    {
        [Key]
        public long Id { get; set; }
        public bool IsDelete { get; set; }
        public long CreateId { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateTime { get; set; }
        public long ModifyId { get; set; }
        public string ModifyName { get; set; }
        public DateTime ModifyTime { get; set; }
    }
}