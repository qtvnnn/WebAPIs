using MISA.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class CustomerGroupService
    {
        public IEnumerable<CustomerGroup> Get()
        {
            var customerGroupRepository = new CustomerGroupRepository();
            throw new Exception();
        }
    }
}
