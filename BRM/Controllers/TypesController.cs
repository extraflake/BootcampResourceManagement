using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRM.Services.Interfaces;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private ITypeService _typeService;

        public TypesController(ITypeService typeService)
        {
            _typeService = typeService;
        }

        [HttpGet]
        public IActionResult GetTypes()
        {
            var get = _typeService.Get();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("{id}")]
        public IActionResult GetType(string id)
        {
            var get = _typeService.Get(id);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpPut]
        public IActionResult InsertType(TypeVM typeVM)
        {
            var push = _typeService.Push(typeVM);
            if (push > 0)
            {
                return Ok(push);
            }
            return BadRequest("Bad Request");
        }
    }
}