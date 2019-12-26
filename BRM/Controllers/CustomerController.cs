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
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;
        MyContext myContext = new MyContext();

        public CustomerController(ICustomerService customerService)
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

        //[HttpGet("{id}")]
        //public IActionResult GetCustomers(string id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var get = _customerService.Get(id);
        //        if (get != null)
        //        {
        //            return Ok(get);
        //        }
        //    }
        //    return NotFound("No Data Found");
        //}

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
    }
}