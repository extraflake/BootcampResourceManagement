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
    public class ParticipantsController : ControllerBase
    {
        private IParticipantService _participantService;

        public ParticipantsController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpGet]
        public IActionResult GetParticipants()
        {
            var get = _participantService.Get();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("{id}")]
        public IActionResult GetParticipants(string id)
        {
            if (ModelState.IsValid)
            {
                var get = _participantService.Get(id);
                if (get != null)
                {
                    return Ok(get);
                }
            }
            return NotFound("No Data Found");
        }

        [HttpPut("InsertParticipant")]
        public IActionResult InsertParticipant(InsertParticipantVM insertParticipantVM)
        {
            if (ModelState.IsValid)
            {
                var push = _participantService.Insert(insertParticipantVM);
                if (push)
                {
                    return Ok("Insert Succesfully");
                }
            }
            return StatusCode(500, "Insert Failed");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteParticipant(string id)
        {
            if (ModelState.IsValid)
            {
                var push = _participantService.Delete(id);
                if (push)
                {
                    return Ok("Delete Succesfully");
                }
            }
            return StatusCode(500, "Delete Failed");
        }
    }
}