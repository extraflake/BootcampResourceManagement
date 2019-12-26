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
    public class BatchesController : ControllerBase
    {
        private IBatchService _batchService;

        public BatchesController(IBatchService batchService)
        {
            _batchService = batchService;
        }

        [HttpGet]
        public IActionResult GetBatches()
        {
            var get = _batchService.Get();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("{id}")]
        public IActionResult GetBatch(string id)
        {
            var get = _batchService.Get(id);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpPost]
        public IActionResult InsertBatch(BatchVM batchVM)
        {
            var result = _batchService.Insert(batchVM);
            if (result)
            {
                return Ok("Successfully Added");
            }
            return BadRequest("Adding Failed");
        }

        [HttpPost("{id}")]
        public IActionResult DeleteBatch(string id, BatchVM batchVM)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return NotFound("No Data Found");
            }
            else
            {
                var result = _batchService.Delete(id, batchVM);
                if (result)
                {
                    return Ok("Successfully Deleted");
                }
            }
            return NotFound("Deleting Failed");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBatch(string id, [FromBody] BatchVM batchVM)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return NotFound("No Data Found");
            }
            else
            {
                var result = _batchService.Update(id, batchVM);
                if (result)
                {
                    return Ok("Successfully Updated");
                }
            }
            return BadRequest("Updating Failed");
        }
    }
}