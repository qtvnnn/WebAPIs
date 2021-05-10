using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Respository
{
    public class BaseReposiotry<T> : IBaseRepository<T>
    {
        IConfiguration _configuration;
        string _connectionString = string.Empty;
        IDbConnection _dbConnection = null;
        string _tableName;
        public BaseReposiotry(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MISACukcukConnectionString");
            _dbConnection = new MySqlConnection(_connectionString);
            _tableName = typeof(T).Name;
        }
        public int Delete(Guid entityId)
        {

            throw new NotImplementedException();

        }

        public IEnumerable<T> Get()
        {
            var entities = _dbConnection.Query<T>($"Proc_Get{_tableName}s", commandType: CommandType.StoredProcedure);
            return entities;
        }

        public T GetById(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public int Insert(T entity)
        {
            var parmeters = MappingDbType(entity);
            var row = _dbConnection.Execute($"Proc_Insert{_tableName}", parmeters, commandType: CommandType.StoredProcedure);

            return row;
        }

        public int Update(T entity)
        {
            throw new NotImplementedException();
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
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }
            return parameters;
        }
    }
}
