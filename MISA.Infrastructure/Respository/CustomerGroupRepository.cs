using Dapper;
using MISA.Core.Services;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure
{
    public class CustomerGroupRepository 
    {
        public IEnumerable<CustomerGroup> Get()
        {
            var connectionString = "Host=47.241.69.179; Port=3306; User Id= dev; Password=12345678; Database= MF0_NVManh_CukCuk02";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            var customerGroups = dbConnection.Query<CustomerGroup>("Proc_GetCustomerGroups", commandType: CommandType.StoredProcedure);
            return customerGroups;
        }
    }
}
