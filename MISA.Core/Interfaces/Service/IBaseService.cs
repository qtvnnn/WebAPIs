using MISA.Core.Entities;
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
        ServiceResult Insert(T entity);
        ServiceResult Update(T entity);
        ServiceResult Delete(Guid entityId);
    }
}
