using BRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantDisplaysController : ControllerBase
    {
        private IParticipantDisplayService _participantDisplayService;
       
        public ParticipantDisplaysController (IParticipantDisplayService participantDisplayService)
        {
            _participantDisplayService = participantDisplayService;
        }

        [HttpGet]
        public IActionResult GetParticipantDisplays()
        {
            var get = _participantDisplayService.Get();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }
    }
}
