using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repository
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> Get();
        T GetById(Guid entityId);
        int Insert(T entity);
        int Update(T entity);
        int Delete(Guid entityId);
        T GetEntityBySpecs(string propertyName, object propertyValue);
    }
}
