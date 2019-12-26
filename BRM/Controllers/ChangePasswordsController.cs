using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRM.Services.Interfaces;
using Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangePasswordsController : ControllerBase
    {
        IChangePasswordService changePasswordService;

        public ChangePasswordsController(IChangePasswordService changePasswordService)
        {
            this.changePasswordService = changePasswordService;
        }

        [HttpGet("GetToken/{email}")]
        public IActionResult GetToken(string email)
        {
            var get = this.changePasswordService.Get(email);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }
    }
}