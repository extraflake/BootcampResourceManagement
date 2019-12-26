using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRM.Services.Interfaces;
using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomerService _customerService;
        MyContext myContext = new MyContext();

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var get = _customerService.GetAll();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }


        [HttpPost]
        public IActionResult InsertCustomer(Customer customer)
        {
            var result = _customerService.Insert(customer);
            if (result)
            {
                return Ok("Successfully Added");
            }
            return BadRequest("Adding Failed");
        }
        [HttpPut("{id}")]
        public IActionResult Update(string id, Customer customer)
        {
            var push = _customerService.Update(id, customer);
            if (push)
            {
                return Ok("Insert Success");
            }
            return StatusCode(500);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var push = _customerService.Delete(id);
            if (push)
            {
                return Ok("Delete Success");
            }
            return StatusCode(500);
        }
    }
}