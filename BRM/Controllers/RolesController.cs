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
    public class RolesController : ControllerBase
    {
        private IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            var get = _roleService.Get();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("{id}")]
        public IActionResult GetRole(int id)
        {
            var get = _roleService.Get(id);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpPost]
        public IActionResult InsertBatch(Role role)
        {
            var result = _roleService.Insert(role);
            if (result)
            {
                return Ok("Successfully Added");
            }
            return BadRequest("Adding Failed");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBatch(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return NotFound("No Data Found");
            }
            else
            {
                var result = _roleService.Delete(id);
                if (result)
                {
                    return Ok("Successfully Deleted");
                }
            }
            return NotFound("Deleting Failed");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBatch(int id, Role role)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return NotFound("No Data Found");
            }
            else
            {
                var result = _roleService.Update(id, role);
                if (result)
                {
                    return Ok("Successfully Updated");
                }
            }
            return BadRequest("Updating Failed");
        }
    }
}