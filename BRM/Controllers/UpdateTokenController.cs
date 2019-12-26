using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BRM.Services.Interfaces;
using ClientSide.Base;
using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateTokenController : ControllerBase
    {
        MyContext myContext = new MyContext();
        Port getPort = new Port();

        private IEmployeeService _employeeService;
        private IAccountService _accountService;
        public UpdateTokenController(IEmployeeService employeeService, IAccountService accountService)
        {
            _employeeService = employeeService;
            _accountService = accountService;
        }

        [HttpPost]
        public IActionResult UpdateToken(string email, string defaultToken, [FromBody] UpdateToken updateToken)
        {
            defaultToken = Guid.NewGuid().ToString();
            email = updateToken.Email;
            Account account = new Account();
            account.token = defaultToken;
            bool get = _employeeService.UpdateToken(email, defaultToken);
            return Ok(200);
        }

        [HttpGet("{email}")]
        public IActionResult GetEmployee(string email)
        {
            var get = _employeeService.Get(email);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }
    }
}