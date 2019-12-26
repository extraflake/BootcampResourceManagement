using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRM.Services.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private IAssetService _assetService;

        public AssetsController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        [HttpGet]
        public IActionResult GetAssets()
        {
            var get = _assetService.Get();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("{id}")]
        public IActionResult GetAsset(string id)
        {
            var get = _assetService.Get(id);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("GetAssetDisplay")]
        public IActionResult GetAssetDisplay()
        {
            var get = _assetService.GetDisplay();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpPut("InsertAsset")]
        public IActionResult InsertAsset(Asset asset)
        {
            var result = _assetService.Insert(asset);
            if (result)
            {
                return Ok("Successfully Added");
            }
            return BadRequest("Adding Failed");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAsset(string id, [FromBody] Asset asset)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return NotFound("No Data Found");
            }
            else
            {
                var result = _assetService.Update(id, asset);
                if (result)
                {
                    return Ok("Successfully Updated");
                }
            }
            return BadRequest("Updating Failed");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBatch(string id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return NotFound("No Data Found");
            }
            else
            {
                var result = _assetService.Delete(id);
                if (result)
                {
                    return Ok("Successfully Deleted");
                }
            }
            return NotFound("Deleting Failed");
        }
    }
}