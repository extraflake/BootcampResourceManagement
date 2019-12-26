using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRM.Services;
using BRM.Services.Interfaces;
using Data.ViewModel;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAssetsController : ControllerBase
    {
        private IEmployeeAssetService _employeeAssetService;

        public EmployeeAssetsController(IEmployeeAssetService employeeAssetService)
        {
            _employeeAssetService = employeeAssetService;
        }

        [HttpGet]
        public IActionResult GetEmployeeAssets()
        {
            var get = _employeeAssetService.Get();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeAssetsById(int id)
        {
            var get = _employeeAssetService.Get(id);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpPost]
        public IActionResult InsertEmployeeAsset(InsertEmployeeAssetVM insertEmployeeAssetVM)
        {
            var push = _employeeAssetService.Insert(insertEmployeeAssetVM);
            if (push)
            {
                return Ok("Insert Success");
            }
            return StatusCode(500);
        }

        //[HttpPut("{id}")]
        //public IActionResult UpdateEmployeeAsset(int id, EmployeeAssetVM employeeAssetVM)
        //{
        //    var push = _employeeAssetService.Update(id, employeeAssetVM);
        //    if (push)
        //    {
        //        return Ok("Insert Success");
        //    }
        //    return StatusCode(500);
        //}
    }
}