using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BRM.Services.Interfaces;
using Data.Context;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace BRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeService _employeeService;
        MyContext myContext = new MyContext();

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var get = _employeeService.Get();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("GetDisplay")]
        public IActionResult GetDisplay()
        {
            var get = _employeeService.GetDisplay();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("GetByIdDisplay/{email}")]
        public IActionResult GetByIdDisplay(string email)
        {
            var get = _employeeService.GetByIdDisplay(email);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("Participant")]
        public IActionResult GetParticipant()
        {
            var get = myContext.participantDisplayVMs.FromSql($"call sp_retrieve_new_employee").ToList();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
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

        [HttpGet("trainer/{id}")]
        public IActionResult GetEmployee(int id)
        {
            var get = _employeeService.GetTrainer(id);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpPut("InsertEmployee")]
        public IActionResult InsertEmployee(EmployeeVM employeeVM)
        {
            var get = _employeeService.Get(employeeVM.Email);
            if (get != null)
            {
                return BadRequest("Adding Failed");
            }
            else
            {
                var result = _employeeService.Insert(employeeVM);
                if (result)
                {
                    return Ok("Successfully Added");
                }
            }
            return BadRequest("Adding Failed");
        }

        [HttpPost("{id}")]
        public IActionResult DeleteEmployee(string id, EmployeeVM employeeVM)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return NotFound("No Data Found");
            }
            else
            {
                var result = _employeeService.Delete(id, employeeVM);
                if (result)
                {
                    return Ok("Successfully Deleted");
                }
            }
            return NotFound("Deleting Failed");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(string id, [FromBody] EmployeeVM employeeVM)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return NotFound("No Data Found");
            }
            else
            {
                var result = _employeeService.Update(id, employeeVM);
                if (result)
                {
                    return Ok("Successfully Updated");
                }
            }
            return BadRequest("Updating Failed");
        }

        [HttpPut("UpdateEmployeeNIK/{email}")]
        public IActionResult UpdateEmployeeNIK(string email, [FromBody] EmployeeVM employeeVM)
        {
            if (string.IsNullOrWhiteSpace(email.ToString()))
            {
                return NotFound("No Data Found");
            }
            else
            {
                var result = _employeeService.Update(email, employeeVM);
                if (result)
                {
                    return Ok("Successfully Updated");
                }
            }
            return BadRequest("Updating Failed");
        }
    }
}

    