using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRM.Services.Interfaces;
using Data.Context;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeRolesController : ControllerBase
    {
        private IEmployeeRoleService _employeeRoleService;

        public EmployeeRolesController(IEmployeeRoleService employeeRoleService)
        {
            _employeeRoleService = employeeRoleService;
        }

        [HttpGet]
        public IActionResult GetEmployeeRoles()
        {
            var get = _employeeRoleService.Get();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeRole(int id)
        {
            var get = _employeeRoleService.Get(id);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }
        [HttpPost]
        public IActionResult InsertEmployeeRole(InsertEmployeeRoleVM employeeRoleVM)
        {
            var result = _employeeRoleService.Insert(employeeRoleVM);
            if (result)
            {
                return Ok("Successfully Added");
            }
            return BadRequest("Adding Failed");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployeeRole(int id, EmployeeRoleVM employeeRoleVM)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return NotFound("No Data Found");
            }
            else
            {
                var result = _employeeRoleService.Update(id, employeeRoleVM);
                if (result)
                {
                    return Ok("Successfully Updated");
                }
            }
            return BadRequest("Updating Failed");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployeeRole(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return NotFound("No Data Found");
            }
            else
            {
                var result = _employeeRoleService.Delete(id);
                if (result)
                {
                    return Ok("Successfully Deleted");
                }
            }
            return NotFound("Deleting Failed");
        }

        public void getEmployee()
        {
            MyContext myContext = new MyContext();
            var get = myContext.Employees.ToList();
        }
    }
}