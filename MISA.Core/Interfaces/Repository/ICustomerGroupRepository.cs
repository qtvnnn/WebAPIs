using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Service.Respository
{
    public interface ICustomerGroupRepository:IBaseRepository<CustomerGroup>
    {
        CustomerGroup GetCustomerGroupByName(string name);
    }
}
