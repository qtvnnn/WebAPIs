using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.WebAPIs.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.WebAPIs.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class CustomerGroupController : ControllerBase
    {
        // GET: api/<CustomerGroup>
        [HttpGet]
        public IActionResult Get()
        {
            var connectionString = "Host=47.241.69.179; Port=3306; User Id= dev; Password=12345678; Database= MF0_NVManh_CukCuk02";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            var customerGroups = dbConnection.Query<CustomerGroup>("Proc_GetCustomerGroups", commandType: CommandType.StoredProcedure);
            return Ok(customerGroups);
        }

        // GET api/<CustomerGroup>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var connectionString = "Host=47.241.69.179; Port=3306; User Id= dev; Password=12345678; Database= MF0_NVManh_CukCuk02";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            var customerGroup = dbConnection.Query<CustomerGroup>("Proc_GetCustomerGroupById", new { CustomerGroupId = id }, commandType: CommandType.StoredProcedure);
            return Ok(customerGroup);
        }

        // POST api/<CustomerGroup>
        [HttpPost]
        public IActionResult Post(CustomerGroup customerGroup)
        {
            var connectionString = "Host=47.241.69.179; Port=3306; User Id= dev; Password=12345678; Database= MF0_NVManh_CukCuk02";
            IDbConnection dbConnection = new MySqlConnection(connectionString);

            //Check trường bắt buộc
            var customerGroupName = customerGroup.CustomerGroupName;
            if (string.IsNullOrEmpty(customerGroupName))
            {
                var msg = new
                {
                    devMsg = new
                    {
                        fieldName = "CustomerGroupName",
                        msg = "Mã khách hàng không được phép để trống"
                    },
                    userMsg = "Mã khách hàng không được phép để trống!",
                    Code = 999
                };
                return BadRequest(msg);
            }
            //check trùng
            var res = dbConnection.Query<CustomerGroup>("Proc_GetCustomerGroupByName", new { CustomerGroupName = customerGroupName }, commandType: CommandType.StoredProcedure);
            if (res.Count() > 0)
            {
                return BadRequest("Tên Nhóm khách hàng đã tồn tại");
            }

            //
            var row = dbConnection.Execute("Proc_InsertCustomerGroup", customerGroup, commandType: CommandType.StoredProcedure);

            if (row > 0)
            {
                return Created("ok", row);
            }
            else
            {
                return NoContent();
            }
        }

        // PUT api/<CustomerGroup>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok(1);
        }

        // DELETE api/<CustomerGroup>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(1);
        }
    }
}
