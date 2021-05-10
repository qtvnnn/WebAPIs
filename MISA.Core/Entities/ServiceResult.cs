using MISA.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class ServiceResult
    {
        public object Data { get; set; }
        public string Messenger { get; set; }
        public MISACode MISAcode { get; set; }
    }
}
