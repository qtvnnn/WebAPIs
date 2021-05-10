using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Service.Respository
{
    public interface ICustomerGroupRepository
    {
        IEnumerable<CustomerGroup> Get();
        CustomerGroup GetById(Guid id);
        int Insert(CustomerGroup customerGroup);
        int Update(CustomerGroup customerGroup);
        int Delete(Guid id);
        CustomerGroup GetCustomerGroupByName(string name);

    }
}
