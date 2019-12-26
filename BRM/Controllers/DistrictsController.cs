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
    public class DistrictsController : ControllerBase
    {
        private IDistrictService _districtService;

        public DistrictsController(IDistrictService districtService)
        {
            _districtService = districtService;
        }

        [HttpGet]
        public IActionResult GetDitricts()
        {
            var get = _districtService.Get();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("{id}")]
        public IActionResult GetDistrict(int id)
        {
            var get = _districtService.Get(id);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("GetDistricts/{param}")]
        public IActionResult GetDistricts(int param)
        {
            var get = _districtService.GetParam(param);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }
    }
}