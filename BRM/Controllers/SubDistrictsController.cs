using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRM.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubDistrictsController : ControllerBase
    {
        private ISubDistrictService _subDistrictService;

        public SubDistrictsController(ISubDistrictService subDistrictService)
        {
            _subDistrictService = subDistrictService;
        }

        [HttpGet]
        public IActionResult GetSubDistricts()
        {
            var get = _subDistrictService.Get();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("{id}")]
        public IActionResult GetSubDistrict(int id)
        {
            var get = _subDistrictService.Get(id);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("GetSubDistricts/{param}")]
        public IActionResult GetSubDistricts(int param)
        {
            var get = _subDistrictService.GetParam(param);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }
    }
}