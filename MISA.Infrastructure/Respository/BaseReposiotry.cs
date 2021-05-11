using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Enum;
using MISA.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Respository
{
    public class BaseReposiotry<T> : IBaseRepository<T> where T : BaseEntity
    {
        IConfiguration _configuration;
        string _connectionString = string.Empty;
        protected IDbConnection _dbConnection = null;
        protected string _tableName;
        public BaseReposiotry(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MISACukcukConnectionString");
            _dbConnection = new MySqlConnection(_connectionString);
            _tableName = typeof(T).Name;
        }
        public int Delete(Guid entityId)
        {
            var res = _dbConnection.Execute($"Proc_Delete{_tableName}", new { CustomerGroupId = entityId }, commandType: CommandType.StoredProcedure);
            return res;
        }

        public IEnumerable<T> Get()
        {
            var entities = _dbConnection.Query<T>($"Proc_Get{_tableName}s", commandType: CommandType.StoredProcedure);
            return entities;
        }

        public T GetById(Guid entityId)
        {
            var customerGroup = _dbConnection.Query<T>($"Proc_Get{_tableName}ById", new { CustomerGroupId = entityId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return customerGroup;
        }

        public int Insert(T entity)
        {
            var parmeters = MappingDbType(entity);
            var row = _dbConnection.Execute($"Proc_Insert{_tableName}", parmeters, commandType: CommandType.StoredProcedure);

            return row;
        }

        public int Update(T entity)
        {
            var parmeters = MappingDbType(entity);
            var row = _dbConnection.Execute($"Proc_Update{_tableName}", parmeters, commandType: CommandType.StoredProcedure);

            return row;
        }

        private DynamicParameters MappingDbType(T entity)
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
                else if (propertyType == typeof(bool) || propertyValue == typeof(bool?))
                {
                    var dbValue = (bool)propertyValue == true ? 1 : 0;
                    parameters.Add($"@{propertyName}", dbValue, DbType.Int32);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }
            return parameters;
        }

        public T GetEntityBySpecs(T entity, PropertyInfo property)
        {
            var propertyName = property.Name;
            var propertyValue = property.GetValue(entity);
            var keyValue = entity.GetType().GetProperty($"{_tableName}Id").GetValue(entity);
            var query = string.Empty;
            if (entity.EntityState == EntityState.Insert)
            {
                query = $"SELECT * FROM {_tableName} WHERE {propertyName} = '{propertyValue}'";
            }
            else if (entity.EntityState == EntityState.Update)
            {
                query = $"SELECT * FROM {_tableName} WHERE {propertyName} = '{propertyValue}' AND {_tableName}Id <> '{keyValue}'";
            }
            else
            {
                return null;
            }
            var entityReturn = _dbConnection.Query<T>(query, commandType: CommandType.Text).FirstOrDefault();
            return entityReturn;
        }
    }
}
