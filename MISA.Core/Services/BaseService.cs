using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        IBaseRepository<T> _baseRepository;
        ServiceResult _serviceResult;
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult() { MISAcode = Enum.MISACode.Success };
        }
        public ServiceResult Delete(Guid entityId)
        {
            _serviceResult.Data = _baseRepository.Delete(entityId);
            return _serviceResult;
        }

        public IEnumerable<T> Get()
        {
            return _baseRepository.Get();
        }

        public T GetById(Guid entityId)
        {
            return _baseRepository.GetById(entityId);
        }

        public virtual ServiceResult Insert(T entity)
        {
            entity.EntityState = Enum.EntityState.Insert;
            //Thực hiện validate
            var isValidate = Validate(entity);
            if (isValidate == true)
            {
                _serviceResult.Data = _baseRepository.Insert(entity);
                _serviceResult.MISAcode = Enum.MISACode.IsValid;
                return _serviceResult;
            }
            else
            {
                return _serviceResult;
            }
        }

        public ServiceResult Update(T entity)
        {
            entity.EntityState = Enum.EntityState.Update;
            var isValidate = Validate(entity);
            if (isValidate == true)
            {
                _serviceResult.Data = _baseRepository.Update(entity);
                _serviceResult.MISAcode = Enum.MISACode.IsValid;
                return _serviceResult;
            }
            else
            {
                return _serviceResult;
            }
        }

        private bool Validate(T entity)
        {
            var mesArrayErro = new List<string>();
            var isValidate = true;
            //Đọc cả property
            var properties = entity.GetType().GetProperties();

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(entity);
                var displayName = string.Empty;
                var displayNameAttributes = property.GetCustomAttributes(typeof(DisplayName), true);
                if (displayNameAttributes.Length > 0)
                {
                    displayName = (displayNameAttributes[0] as DisplayName).Name;
                }
                //Kiểm tra xem có attribute cần phải validate không:
                if (property.IsDefined(typeof(Required), false))
                {
                    //check bắt buộc nhập
                    if ((string)propertyValue == "")
                    {
                        isValidate = false;
                        mesArrayErro.Add(string.Format(Properties.Resources.Msg_Required, displayName));
                        _serviceResult.MISAcode = Enum.MISACode.NotValid;
                        _serviceResult.Messenger = Properties.Resources.Msg_IsNotValid;
                    }
                }
                if (property.IsDefined(typeof(Duplicate), false))
                {
                    //check trùng dữ liệu
                    var propertyName = property.Name;
                    var entityDuplicate = _baseRepository.GetEntityBySpecs(entity, property);
                    if (entityDuplicate != null)
                    {
                        isValidate = false;
                        mesArrayErro.Add(string.Format(Properties.Resources.Msg_Duplicate, displayName));
                        _serviceResult.MISAcode = Enum.MISACode.NotValid;
                        _serviceResult.Messenger = Properties.Resources.Msg_IsNotValid;
                    }
                }
            }
            _serviceResult.Data = mesArrayErro;
            return isValidate;
        }
    }
}
