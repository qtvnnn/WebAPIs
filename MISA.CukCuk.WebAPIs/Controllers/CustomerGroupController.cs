using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Enum;
using MISA.Core.Interfaces;
using MISA.Core.Interfaces.Service;
using MISA.Core.Services;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.WebAPIs.Controllers
{
    public class CustomerGroupController : BaseEntityController<CustomerGroup>
    {
        IBaseService<CustomerGroup> _baseService;
        public CustomerGroupController(IBaseService<CustomerGroup> baseService) : base(baseService)
        {
            _baseService = baseService;
        }
    }
}
