using Microsoft.AspNetCore.Mvc;
using MISA.Core.Enum;
using MISA.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.WebAPIs.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public abstract class BaseEntityController<T> : ControllerBase
    {
        IBaseService<T> _baseService;
        public BaseEntityController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }
        // GET: api/<CustomerGroup>
        [HttpGet]
        public IActionResult Get()
        {
            var entities = _baseService.Get();
            return Ok(entities);
        }

        // GET api/<CustomerGroup>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var entity = _baseService.GetById(id);
            return Ok(entity);
        }

        // POST api/<CustomerGroup>
        [HttpPost]
        public IActionResult Post(T entity)
        {
            var row = _baseService.Insert(entity);
            return Ok(row);
        }

        // PUT api/<CustomerGroup>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] string id, [FromBody] T entity)
        {
            var keyProperty = entity.GetType().GetProperty($"{typeof(T).Name}Id");
            if (keyProperty.PropertyType == typeof(Guid))
            {
                keyProperty.SetValue(entity, Guid.Parse(id));
            }
            else if (keyProperty.PropertyType == typeof(int))
            {
                keyProperty.SetValue(entity, int.Parse(id));
            }
            else
            {
                keyProperty.SetValue(entity, id);
            }

            var row = _baseService.Update(entity);
            return Ok(row);
        }

        // DELETE api/<CustomerGroup>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var res = _baseService.Delete(id);
            return Ok(res);
        }
    }
}
