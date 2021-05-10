using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Service
{
    public interface IBaseService<T>
    {
        IEnumerable<T> Get();
        T GetById(Guid entityId);
        int Insert(T entity);
        int Update(T entity);
        int Delete(Guid entityId);
    }
}
