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
    public class AccountsController : ControllerBase
    {
        private IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public IActionResult GetAccount(UserCredentialVM userCredentialVM)
        {
            var get = _accountService.Get(userCredentialVM);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("{email}/{password}")]
        public IActionResult GetAccounts(string email, string password)
        {
            var get = _accountService.Gets(email, password);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpPut("UpdateChangePassword/{token}")]
        public IActionResult UpdateChangePassword(string token, ChangePasswordVM changePasswordVM)
        {
            if (ModelState.IsValid)
            {
                var push = _accountService.UpdateChangePassword(token, changePasswordVM);
                if (push)
                {
                    return Ok("Update Succesfully");
                }
            }
            return StatusCode(500, "Update Failed");
        }

    }
}