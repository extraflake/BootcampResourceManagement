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
    public class BatchClassesController : ControllerBase
    {
        private IBatchClassService _batchClassService;

        public BatchClassesController(IBatchClassService batchClassService)
        {
            _batchClassService = batchClassService;
        }

        [HttpGet]
        public IActionResult GetBatchClasses()
        {
            var get = _batchClassService.Get();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("{id}")]
        public IActionResult GetBatchClass(string id)
        {
            if (ModelState.IsValid)
            {
                var get = _batchClassService.Get(id);
                if (get != null)
                {
                    return Ok(get);
                }
            }
            return NotFound("No Data Found");
        }

        [HttpPut("InsertBatchClass")]
        public IActionResult InsertBatchClass(BatchClassVM batchClassVM)
        {
            if (ModelState.IsValid)
            {
                var push = _batchClassService.Insert(batchClassVM);
                if (push)
                {
                    return Ok("insert succesfully");
                }
            }
            return StatusCode(500, "Insert Failed");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBatchClass(string id, BatchClassVM batchClassVM)
        {
            if (ModelState.IsValid)
            {
                var push = _batchClassService.Update(id, batchClassVM);
                if (push)
                {
                    return Ok("Update Succesfully");
                }
            }
            return StatusCode(500, "Update Failed");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBatchClass(string id)
        {
            if (ModelState.IsValid)
            {
                var push = _batchClassService.Delete(id);
                if (push)
                {
                    return Ok("Delete Succesfully");
                }
            }
            return StatusCode(500, "Delete Failed");
        }
    }
}