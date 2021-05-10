using Dapper;
using MISA.Core.Entities;
using MISA.Core.Enum;
using MISA.Core.Interfaces;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Service;
using MISA.Core.Interfaces.Service.Respository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class CustomerGroupService : BaseService<CustomerGroup>, ICustomerGroupService
    {
        IBaseRepository<CustomerGroup> _customerGroupRepository;
        public CustomerGroupService(IBaseRepository<CustomerGroup> customerGroupRepository) : base(customerGroupRepository)
        {
            _customerGroupRepository = customerGroupRepository;
        }

        //public ServiceResult Delete(Guid id)
        //{
        //    var ServiceResult = new ServiceResult();
        //    ServiceResult.Data = _customerGroupRepository.Delete(id);
        //    return ServiceResult;
        //}

        //public CustomerGroup GetById(Guid id)
        //{
        //    var customerGroup = _customerGroupRepository.GetById(id);
        //    return customerGroup;
        //}

        //public IEnumerable<CustomerGroup> Get()
        //{
        //    var customerGroups = _customerGroupRepository.Get();
        //    return customerGroups;
        //}

        //public ServiceResult Insert(CustomerGroup customerGroup)
        //{
        //    var serviceResult = new ServiceResult();
        //    //validate du lieu

        //    //
        //    var connectionString = "Host=47.241.69.179; Port=3306; User Id= dev; Password=12345678; Database= MF0_NVManh_CukCuk02";
        //    IDbConnection dbConnection = new MySqlConnection(connectionString);

        //    //Check trường bắt buộc
        //    var customerGroupName = customerGroup.CustomerGroupName;
        //    if (string.IsNullOrEmpty(customerGroupName))
        //    {
        //        var msg = new
        //        {
        //            devMsg = new
        //            {
        //                fieldName = "CustomerGroupName",
        //                msg = "Mã khách hàng không được phép để trống"
        //            },
        //            userMsg = "Mã khách hàng không được phép để trống!",
        //            Code = MISACode.NotValid
        //        };
        //        serviceResult.MISAcode = MISACode.NotValid;
        //        serviceResult.Messenger = "Mã khách hàng không được phép để trống!";
        //        serviceResult.Data = msg;
        //        return serviceResult;
        //    }
        //    //check trùng
        //    var res = _customerGroupRepository.GetCustomerGroupByName(customerGroupName);
        //    if (res != null)
        //    {
        //        var msg = new
        //        {
        //            devMsg = new
        //            {
        //                fieldName = "CustomerGroupName",
        //                msg = "Mã khách hàng đã tồn tại!"
        //            },
        //            userMsg = "Mã khách hàng đã tồn tại!",
        //            Code = MISACode.NotValid
        //        };
        //        serviceResult.MISAcode = MISACode.NotValid;
        //        serviceResult.Messenger = "Mã khách hàng đã tồn tại!";
        //        serviceResult.Data = msg;
        //        return serviceResult;
        //    }

        //    var row = _customerGroupRepository.Insert(customerGroup);
        //    serviceResult.MISAcode = MISACode.IsValid;
        //    serviceResult.Messenger = "Thêm thành công";
        //    serviceResult.Data = row;
        //    return serviceResult;
        //}

        //public ServiceResult Update(CustomerGroup customerGroup)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
