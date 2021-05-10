using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Service.Respository;
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
    public class CustomerGroupRepository : ICustomerGroupRepository
    {
        IConfiguration _configuration;
        string _connectionString = string.Empty;
        IDbConnection _dbConnection = null;
        public CustomerGroupRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MISACukcukConnectionString");
            _dbConnection = new MySqlConnection(_connectionString);
        }
        public CustomerGroup GetCustomerGroupByName(string name)
        {
            var res = _dbConnection.Query<CustomerGroup>("Proc_GetCustomerGroupByName", new { CustomerGroupName = name }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return res;
        }
        public int Delete(Guid id)
        {
            var res = _dbConnection.Execute("Proc_DeleteCustomerGroup", new { CustomerGroupId = id }, commandType: CommandType.StoredProcedure);
            return res;
        }

        public IEnumerable<CustomerGroup> Get()
        {
            var customerGroups = _dbConnection.Query<CustomerGroup>("Proc_GetCustomerGroups", commandType: CommandType.StoredProcedure);
            return customerGroups;
        }

        public CustomerGroup GetById(Guid id)
        {
            var customerGroup = _dbConnection.Query<CustomerGroup>("Proc_GetCustomerGroupById", new { CustomerGroupId = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return customerGroup;
        }

        public int Insert(CustomerGroup customerGroup)
        {
            var parmeters = MappingDbType(customerGroup);
            var row = _dbConnection.Execute("Proc_InsertCustomerGroup", parmeters, commandType: CommandType.StoredProcedure);

            return row;
        }

        public int Update(CustomerGroup customerGroup)
        {
            var parmeters = MappingDbType(customerGroup);
            var row = _dbConnection.Execute("Proc_UpdateCustomerGroup", parmeters, commandType: CommandType.StoredProcedure);

            return row;
        }

        private DynamicParameters MappingDbType<T>(T entity)
        {
            var properties = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;
                if (propertyType == typeof(Guid) || propertyValue == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }
            return parameters;
        }
    }
}
