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
    public class RoomsController : ControllerBase
    {
        private IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public IActionResult GetRooms()
        {
            var get = _roomService.Get();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("{id}")]
        public IActionResult GetRoom(string id)
        {
            var get = _roomService.Get(id);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }
    }
}