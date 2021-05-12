using MISA.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : Attribute
    {

    }
    [AttributeUsage(AttributeTargets.Property)]
    public class Duplicate : Attribute
    {

    }
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayName : Attribute
    {
        public string Name { get; set; }
        public DisplayName(string name = null)
        {
            this.Name = name;
        }
    }
    public class BaseEntity
    {
        public EntityState EntityState { get; set; } = EntityState.Insert;
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
