using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRM.Services.Interfaces;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacementsController : ControllerBase
    {
        private IPlacementService _placementService;

        public PlacementsController(IPlacementService placementService)
        {
            _placementService = placementService;
        }

        [HttpGet]
        public IActionResult GetPlacements()
        {
            var get = _placementService.GetAll();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("{id}")]
        public IActionResult GetPlacement(int id)
        {
            if (ModelState.IsValid)
            {
                var get = _placementService.GetIdVM(id);
                if (get != null)
                {
                    return Ok(get);
                }
            }
            return NotFound("No Data Found");
        }

        [HttpPut("GetPlacementRmandCus")]
        public IActionResult GetPlacementRmandCus(PlacementParamVM placementParamVM)
        {
            if (ModelState.IsValid)
            {
                var get = _placementService.GetRmandGetCus(placementParamVM);
                if (get != null)
                {
                    return Ok(get);
                }
            }
            return NotFound("No Data Found");
        }

        [HttpPut("InsertPlacement")]
        public IActionResult InsertPlacement(InsertPlacementVM insertPlacementVM)
        {
            if (ModelState.IsValid)
            {
                var push = _placementService.Insert(insertPlacementVM);
                if (push)
                {
                    return Ok("Insert Succesfully");
                }
            }
            return StatusCode(500, "Insert Failed");
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlacement(int id, Placement placement)
        {
            if (ModelState.IsValid)
            {
                var push = _placementService.Update(id, placement);
                if (push)
                {
                    return Ok("Update Succesfully");
                }
            }
            return StatusCode(500, "Update Failed");
        }

        [HttpPut("Finish/{id}")]
        public IActionResult FinishDatePlacement(int id, Placement placement)
        {
            if (ModelState.IsValid)
            {
                var push = _placementService.FinishDate(id, placement);
                if (push)
                {
                    return Ok("Update Succesfully");
                }
            }
            return StatusCode(500, "Update Succesfully");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlacement(int id)
        {
            if (ModelState.IsValid)
            {
                var push = _placementService.Delete(id);
                if (push)
                {
                    return Ok("Delete Succesfully");
                }
            }
            return StatusCode(500, "Delete Failed");
        }
    }
}