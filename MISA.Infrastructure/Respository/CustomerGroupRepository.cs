using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Service.Respository;
using MISA.Infrastructure.Respository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure
{
    public class CustomerGroupRepository : BaseReposiotry<CustomerGroup>, ICustomerGroupRepository
    {
        public CustomerGroupRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public CustomerGroup GetCustomerGroupByName(string name)
        {
            var res = _dbConnection.Query<CustomerGroup>("Proc_GetCustomerGroupByName", new { CustomerGroupName = name }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return res;
        }
    }
}
