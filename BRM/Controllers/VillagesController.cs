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
    public class VillagesController : ControllerBase
    {
        private IVillageService _villageService;

        public VillagesController(IVillageService villageService)
        {
            _villageService = villageService;
        }

        [HttpGet]
        public IActionResult GetVillages()
        {
            var get = _villageService.Get();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("{id}")]
        public IActionResult GetVillage(int id)
        {
            var get = _villageService.Get(id);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }
    }
}